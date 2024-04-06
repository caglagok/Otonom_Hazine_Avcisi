using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace prolab1.Core
{
    public class Kus : DinamikEngel
    {
        private Random random = new Random();
        private int hucreBoyutu;
        private Panel panel;
        private int kenarBoyutu;
        private Timer timer;
        private int hareketMiktari = 5;
        private int kalanYol = 5;
        private bool hareketYonuAsagi = true;

        public Point Konum { get; set; }

        public Kus(int hucreBoyutu, Panel panel, int kenarBoyutu) : base(hucreBoyutu, panel, kenarBoyutu, "Kus") 
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;
            this.kenarBoyutu = kenarBoyutu;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            KusuHareketEttir();
        }

        private void KusuHareketEttir()
        {
            int yeniY = Konum.Y;

            if (kalanYol > 0)
            {
                yeniY -= hareketMiktari;
                kalanYol -= hareketMiktari;
            }
            else if (kalanYol > -10)
            {
                yeniY += hareketMiktari;
                kalanYol -= hareketMiktari;
            }
            else
            { 
                yeniY -= hareketMiktari;
                kalanYol = 5;
            }

            Konum = new Point(Konum.X, yeniY);
            GuncelleKusPictureBoxKonum();
        }

        private void GuncelleKusPictureBoxKonum()
        {
            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox && control.Tag != null && control.Tag.ToString() == "Kus")
                {
                    control.Location = new Point(Konum.X * hucreBoyutu, Konum.Y * hucreBoyutu);
                    break;
                }
            }
        }

        public void KusAtama()
        {
            Point kusKonumu = RastgeleKonumAl(2, 2);

            while (!GecerliPoz(kusKonumu, 2, 2))
            {
                kusKonumu = RastgeleKonumAl(2, 2);
            }

            this.Konum = kusKonumu; 

            PictureBox kusPictureBox = new PictureBox
            {
                Size = new Size(hucreBoyutu * 2, hucreBoyutu * 2),
                Location = new Point(Konum.X * hucreBoyutu, Konum.Y * hucreBoyutu),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "Asset", "Kus", "kus.gif")) ?? throw new FileNotFoundException("Image file not found"),
                Tag = "Kus"
            };

            panel.Controls.Add(kusPictureBox);
        }

        protected override Point RastgeleKonumAl(int resimGenislik, int resimYukseklik)
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

        protected override bool GecerliPoz(Point position, int resimGenislik, int resimYukseklik)
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

        protected override bool SandıkEngelleniyorMu(Point chestLocation)
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