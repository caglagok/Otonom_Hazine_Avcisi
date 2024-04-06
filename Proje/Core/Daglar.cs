using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public class Daglar : HareketsizEngeller
    { 
        private Random random = new Random();
        private List<string> kısdaglarResimler;
        private List<string> yazdaglarResimler;
        private int hucreBoyutu;
        private Panel panel;
        private int kenarBoyutu;
        public Daglar(int hucreBoyutu, Panel panel, int kenarBoyutu) : base(hucreBoyutu, panel, kenarBoyutu, "Daglar") 
        {
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;
            this.kenarBoyutu = kenarBoyutu;
        }

        public void DagAtama()
        {
            kısdaglarResimler = new List<string>
            {
                "kısdag2.png",
                "kısdag3.png"
            };
            yazdaglarResimler = new List<string>
            {
                "yazdag1.jpeg",
                "yazdag3.jpeg"
            };

            for (int i = 0; i < 4; i++)
            {
                string kısdaglarResimYolu = kısdaglarResimler[i % kısdaglarResimler.Count];
                string yazdaglarResimYolu = yazdaglarResimler[i % yazdaglarResimler.Count];

                Point daglarKonumu;
                do
                {
                    daglarKonumu = RastgeleKonumAl(6,6);
                } while (SandıkEngelleniyorMu(daglarKonumu));

                PictureBox daglarPictureBox = new PictureBox
                {
                    Size = new Size(hucreBoyutu * 6, hucreBoyutu * 6),
                    Location = new Point(daglarKonumu.X * hucreBoyutu, daglarKonumu.Y * hucreBoyutu),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "Dag"
                };

                if (daglarKonumu.X < kenarBoyutu / 2)
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Dag", "KısDag", kısdaglarResimYolu);
                    daglarPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }
                else
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Dag", "YazDag", yazdaglarResimYolu);
                    daglarPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }

                panel.Controls.Add(daglarPictureBox);
                daglarPictureBox.BringToFront();
            }
            
        }

        public Point RastgeleKonumAl(int resimGenislik, int resimYukseklik)
        {
            if (hucreBoyutu == 0)
            {
                return new Point(0, 0);
            }

            int maxX = panel.Width / hucreBoyutu;
            int maxY = panel.Height / hucreBoyutu;

            bool validPositionFound = false;
            Point randomPosition = Point.Empty;

            while (!validPositionFound)
            {
                int randomX = random.Next(maxX);
                int randomY = random.Next(maxY);

                randomPosition = new Point(randomX, randomY);

                bool positionValid = GecerliPoz(randomPosition, resimGenislik, resimYukseklik);

                if (positionValid)
                {
                    validPositionFound = true;
                }
            }

            return randomPosition;
        }

        private bool GecerliPoz(Point position, int resimGenislik, int resimYukseklik)
        {
            Rectangle imageRect = new Rectangle(position.X * hucreBoyutu, position.Y * hucreBoyutu, resimGenislik * hucreBoyutu, resimYukseklik * hucreBoyutu);

            foreach (Control control in panel.Controls)
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

            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox)
                {
                    Rectangle controlRect = new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height);
                    if (controlRect.IntersectsWith(imageRect) && control.Tag != null && control.Tag.ToString() == "Engel")
                    {
                        return false; 
                    }
                }
            }

            return true;
        }

        private bool SandıkEngelleniyorMu(Point chestLocation)
        {
            int chestX = chestLocation.X;
            int chestY = chestLocation.Y;

            for (int i = chestX - 1; i <= chestX + 1; i++)
            {
                for (int j = chestY - 1; j <= chestY + 1; j++)
                {
                    if (i >= 0 && i < kenarBoyutu && j >= 0 && j < kenarBoyutu &&
                        panel.Controls.OfType<PictureBox>().Any(pictureBox =>
                        {
                            int pictureBoxX = pictureBox.Location.X / hucreBoyutu;
                            int pictureBoxY = pictureBox.Location.Y / hucreBoyutu;
                            return pictureBoxX == i && pictureBoxY == j && pictureBox.Tag != null && pictureBox.Tag.ToString() == "Sandik";
                        }))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

