using prolab1.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prolab1
{
    public partial class Uygulama : Form
    {
        private readonly Random random = new Random();
        public int hucreBoyutu = 15;
        private PictureBox karakterPictureBox;
        private Point karakterKonumu;
        private int kenarBoyutu;
        private List<Point> kullanılanPoz = new List<Point>();
        public Uygulama()
        {
            InitializeComponent();
            karakterPictureBox = new PictureBox();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out kenarBoyutu))
            {
                OluşturGrid(kenarBoyutu);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.");
            }
        }

        public void OluşturGrid(int kenarBoyutu)
        {
            panel1.Controls.Clear();

            this.kenarBoyutu = kenarBoyutu;
            karakterKonumu = RastgeleKonumAl(karakterPictureBox.Width, karakterPictureBox.Height);

            karakterPictureBox.Location = CakısmayanPoz(karakterPictureBox.Size);
            kullanılanPoz.Add(karakterPictureBox.Location);
            string karakterResimYolu = Path.Combine(Application.StartupPath, "Asset", "Karakter", "karakter.jpeg");

            karakterPictureBox = new PictureBox
            {
                Size = new Size(hucreBoyutu, hucreBoyutu),
                Location = new Point(karakterKonumu.X * hucreBoyutu, karakterKonumu.Y * hucreBoyutu),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(karakterResimYolu) ?? throw new FileNotFoundException("Image file not found")
            };
            
            while (Engelleme(karakterKonumu) || IsCharacterCollision(karakterKonumu))
            {
                karakterKonumu = RastgeleKonumAl(karakterPictureBox.Width, karakterPictureBox.Height);
                karakterPictureBox.Location = new Point(karakterKonumu.X * hucreBoyutu, karakterKonumu.Y * hucreBoyutu);
            }

            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox && (control.Location.X == karakterPictureBox.Location.X && control.Location.Y == karakterPictureBox.Location.Y))
                {
                    karakterKonumu = RastgeleKonumAl(karakterPictureBox.Width, karakterPictureBox.Height);
                    karakterPictureBox.Location = new Point(karakterKonumu.X * hucreBoyutu, karakterKonumu.Y * hucreBoyutu);
                    break;
                }
            }

            panel1.Controls.Add(karakterPictureBox);
            karakterPictureBox.BringToFront();

            Sandık sandiklar = new Sandık(hucreBoyutu, panel1);
            sandiklar.SandıkAtama();

            Daglar daglar = new Daglar(hucreBoyutu, panel1, kenarBoyutu);
            daglar.DagAtama();

            Kayalar kayalar = new Kayalar(hucreBoyutu, panel1, kenarBoyutu);
            kayalar.KayalarAtama();

            Agaclar agaclar = new Agaclar(hucreBoyutu, panel1, kenarBoyutu);
            agaclar.AgaclarAtama();

            Duvarlar duvarlar = new Duvarlar(hucreBoyutu, panel1, kenarBoyutu);
            duvarlar.DuvarAtama();

            Kus kus = new Kus(hucreBoyutu, panel1, kenarBoyutu);
            kus.KusAtama();

            Ari ari = new Ari(hucreBoyutu, panel1, kenarBoyutu);
            ari.AriAtama();

            kullanılanPoz.Add(karakterPictureBox.Location);


            for (int i = 0; i < kenarBoyutu; i++)
            {
                for (int j = 0; j < kenarBoyutu; j++)
                {
                    Panel renkliPanel = new Panel
                    {
                        Size = new Size(hucreBoyutu, hucreBoyutu),
                        Location = new Point(j * hucreBoyutu, i * hucreBoyutu),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    if (j < kenarBoyutu / 2)
                    {
                        panel1.BackColor = Color.AliceBlue;
                    }
                    else
                    {
                        renkliPanel.BackColor = Color.Khaki;
                    }

                    panel1.Controls.Add(renkliPanel);
                }
            }

            for (int i = kus.Konum.Y - 5; i <= kus.Konum.Y + 6; i++)
            {
                for (int j = kus.Konum.X; j <= kus.Konum.X + 1; j++) 
                {
                    Panel kusPanel = panel1.Controls
                         .OfType<Panel>()
                         .FirstOrDefault(panel => panel.Location.X == j * hucreBoyutu && panel.Location.Y == i * hucreBoyutu);
                    if (kusPanel != null)
                    {
                        kusPanel.BackColor = Color.Red;
                    }
                }
            }

            for (int i = ari.Konum.Y; i <= ari.Konum.Y + 1; i++)
            {
                for (int j = ari.Konum.X - 3; j <= ari.Konum.X + 4; j++)
                {
                    Panel ariPanel = panel1.Controls
                        .OfType<Panel>()
                        .FirstOrDefault(panel => panel.Location.X == j * hucreBoyutu && panel.Location.Y == i * hucreBoyutu);
                    if (ariPanel != null)
                    {
                        ariPanel.BackColor = Color.Red;
                    }
                }
            }

        }

        private bool Engelleme(Point HedefPoz)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox && control.Tag != null)
                {
                    string tag = control.Tag.ToString();
                    if (tag == "Duvar" || tag == "Kayalık" || tag == "Dag" || tag == "Agac" || tag == "Kus" || tag == "Ari")
                    {
                        int obstacleX = control.Location.X / hucreBoyutu;
                        int obstacleY = control.Location.Y / hucreBoyutu;

                        if (HedefPoz.X == obstacleX && HedefPoz.Y == obstacleY)
                        { 
                            listBox1.Items.Add($"Engelle çakışma: {tag}"); 
                            return true;
                        }
                    }
                }
            }
            return false; 

        }

        private Point CakısmayanPoz(Size imageSize)
        {
            Point position = RastgeleKonumAl(imageSize.Width, imageSize.Height);

            while (kullanılanPoz.Any(p => p == position))
            {
                position = RastgeleKonumAl(imageSize.Width, imageSize.Height);
            }

            return position;
        }
        public Point RastgeleKonumAl(int resimGenislik, int resimYukseklik)
        {
            if (hucreBoyutu == 0)
            {
                return new Point(0, 0);
            }

            int maxX = panel1.Width / hucreBoyutu;
            int maxY = panel1.Height / hucreBoyutu;

            bool gecerlipozbul = false;
            Point randomPoz = Point.Empty;

            while (!gecerlipozbul)
            {
                int randomX = random.Next(maxX);
                int randomY = random.Next(maxY);

                randomPoz = new Point(randomX, randomY);

                bool positionValid = GecerliPoz(randomPoz, resimGenislik, resimYukseklik);

                if (positionValid)
                {
                    gecerlipozbul = true;
                }
            }

            return randomPoz;
        }

        private bool GecerliPoz(Point pozisyon, int resimGenislik, int resimYukseklik)
        {
            Rectangle imageRect = new Rectangle(pozisyon.X * hucreBoyutu, pozisyon.Y * hucreBoyutu, resimGenislik * hucreBoyutu, resimYukseklik * hucreBoyutu);

            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox)
                {
                    Rectangle controlRect = new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height);
                    if (controlRect.IntersectsWith(imageRect))
                    {
                        return false;
                    }
                }
            }

            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox)
                {
                    Rectangle controlRect = new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height);
                    if (controlRect.IntersectsWith(imageRect) && control.Tag != null && control.Tag.ToString() == "Duvar" && control.Tag.ToString() == "Agac" && control.Tag.ToString() == "Dag" && control.Tag.ToString() == "Kayalık" && control.Tag.ToString() == "Kus" && control.Tag.ToString() == "Ari")
                    {
                        return false;
                    }
                }
            }

            Rectangle expandedRect = imageRect;
            expandedRect.Inflate(hucreBoyutu, hucreBoyutu);

            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox && control != karakterPictureBox)
                {
                    Rectangle controlRect = new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height);
                    if (expandedRect.IntersectsWith(controlRect))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool YeniGecerliPoz(Point newPoz)
        {
            if (Engelleme(newPoz))
            {
                return false;
            }

            if (newPoz.X < 0 || newPoz.X >= kenarBoyutu || newPoz.Y < 0 || newPoz.Y >= kenarBoyutu)
            {
                return false;
            }

            return true;
        }

        private void YesilBoyamal(int x, int y)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is Panel && control.Location.X == x * (hucreBoyutu) && control.Location.Y == y * (hucreBoyutu))
                {
                    control.BackColor = Color.Green;

                    karakterPictureBox.BringToFront();
                }
            }
        }

        private List<Point> KısaYolBulma(Point start, Point target)
        {
            Dictionary<Point, Point> previous = new Dictionary<Point, Point>();
            HashSet<Point> visited = new HashSet<Point>();
            Queue<Point> queue = new Queue<Point>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();

                if (current == target)
                {
                    List<Point> path = new List<Point>();
                    while (current != start)
                    {
                        path.Insert(0, current);
                        current = previous[current];
                    }
                    return path;
                }

                foreach (Point neighbor in KomsuHucreler(current))
                {

                    if (!visited.Contains(neighbor) && HucreGenisletme(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        previous[neighbor] = current;
                    }

                }
            }
            return null;
        }
        private bool HucreGenisletme(Point cell)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox && control.Tag != null && (control.Tag.ToString() == "Duvar" || control.Tag.ToString() == "Kayalık" || control.Tag.ToString() == "Dag" || control.Tag.ToString() == "Agac" || control.Tag.ToString() == "Kus" || control.Tag.ToString() == "Ari"))
                {
                    Rectangle obstacleRect = new Rectangle(control.Location, control.Size);
                    Rectangle cellRect = new Rectangle(cell.X * hucreBoyutu, cell.Y * hucreBoyutu, hucreBoyutu, hucreBoyutu);
                    if (obstacleRect.IntersectsWith(cellRect))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private async Task EnYakınSandıkBu()
        {
            while (SandıkEksikMı())
            {
                var crates = panel1.Controls
                .OfType<PictureBox>()
                .Where(pictureBox => pictureBox.Tag != null && pictureBox.Tag.ToString() == "Sandik");

                Point characterPos = new Point(karakterPictureBox.Location.X / hucreBoyutu, karakterPictureBox.Location.Y / hucreBoyutu);

                int shortestDistance = int.MaxValue;
                PictureBox targetCrate = null;

                foreach (var crate in crates)
                {
                    Point cratePosition = new Point(crate.Location.X / hucreBoyutu, crate.Location.Y / hucreBoyutu);
                    List<Point> path = KısaYolBulma(characterPos, cratePosition);

                    if (path != null && path.Count < shortestDistance)
                    {
                        shortestDistance = path.Count;
                        targetCrate = crate;
                    }
                }
                if (targetCrate != null)
                {
                    List<Point> pathToTargetCrate = KısaYolBulma(characterPos, new Point(targetCrate.Location.X / hucreBoyutu, targetCrate.Location.Y / hucreBoyutu));

                    if (pathToTargetCrate != null && pathToTargetCrate.Count > 0)
                    {
                        await KarakterYolBoyuHareket(pathToTargetCrate);
                        characterPos = pathToTargetCrate.Last();
                        int crateX = targetCrate.Location.X / hucreBoyutu;
                        int crateY = targetCrate.Location.Y / hucreBoyutu;
                        MessageBox.Show($"Sandık bulundu. Konumu ({crateX}, {crateY}).");
                        listBox1.Items.Add($"Sandık bulundu. Konumu ({crateX}, {crateY}).");

                        panel1.Controls.Remove(targetCrate);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        
        private Point YeniYonBelirle()
        {
            Random random = new Random();
            int rastgeleYon = random.Next(4);

            Point yeniYon = Point.Empty;

            switch (rastgeleYon)
            {
                case 0:
                    yeniYon = new Point(0, -1); 
                    break;
                case 1:
                    yeniYon = new Point(0, 1);
                    break;
                case 2:
                    yeniYon = new Point(-1, 0); 
                    break;
                case 3:
                    yeniYon = new Point(1, 0); 
                    break;
                default:
                    break;
            }

            return yeniYon;
        }
        
        private async Task KarakterYolBoyuHareket(List<Point> path)
        {
            foreach (Point nextPosition in path)
            {
                if (!Engelleme(nextPosition))
                {
                    karakterPictureBox.Location = new Point(nextPosition.X * hucreBoyutu, nextPosition.Y * hucreBoyutu);
                    karakterKonumu = nextPosition;
                    YesilBoyamal(nextPosition.X, nextPosition.Y);
                    panel1.Refresh();
                    await Task.Delay(500);
                }
                else
                {
                    continue;
                }
            }
        }
        private List<Point> KomsuHucreler(Point current)
        {
            List<Point> komsuhucreler = new List<Point>
            {
                new Point(current.X - 1, current.Y),
                new Point(current.X + 1, current.Y),
                new Point(current.X, current.Y - 1),
                new Point(current.X, current.Y + 1)
            };

            return komsuhucreler.Where(cell =>
                cell.X >= 0 && cell.X < kenarBoyutu &&
                cell.Y >= 0 && cell.Y < kenarBoyutu).ToList();
        }
        private bool IsCharacterCollision(Point targetPosition)
        {
            Rectangle characterBounds = new Rectangle(karakterPictureBox.Location, karakterPictureBox.Size);

            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox && control != karakterPictureBox)
                {
                    Rectangle obstacleBounds = new Rectangle(control.Location, control.Size);

                    if (characterBounds.IntersectsWith(obstacleBounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool SandıkEksikMı()
        {
            return panel1.Controls
                .OfType<PictureBox>()
                .Any(pictureBox => pictureBox.Tag != null && pictureBox.Tag.ToString() == "Sandik");
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            await EnYakınSandıkBu();
        }

    }
}