using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public class Agaclar : HareketsizEngeller
    {
        private Random random = new Random();
        private List<string> kısagaclarResimler;
        private List<string> yazagaclarResimler;
        private int hucreBoyutu;
        private Panel panel;
        private int kenarBoyutu;
        public Agaclar(int hucreBoyutu, Panel panel, int kenarBoyutu) : base(hucreBoyutu, panel, kenarBoyutu, "Agaclar")
        {
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;
            this.kenarBoyutu = kenarBoyutu;
        }

        public void AgaclarAtama()
        {
            kısagaclarResimler = new List<string>
            {
                "kısagac1.jpeg",
                "kısagac2.jpeg",
                "kısagac3.jpeg",
                "kısagac4.jpeg"
            };
            yazagaclarResimler = new List<string>
            {
                "yazagac1.jpeg",
                "yazagac2.jpeg",
                "yazagac3.jpeg",
                "yazagac4.jpg"
            };

            int[] farkliBoyutlar = new int[] { 2, 3, 4, 5 };
            int farkliBoyutIndex = 0;

            for (int i = 0; i < 12; i++)
            {
                string kısagaclarResimYolu = kısagaclarResimler[i % kısagaclarResimler.Count];
                string yazagaclarResimYolu = yazagaclarResimler[i % yazagaclarResimler.Count];

                int agacBoyutu = farkliBoyutlar[farkliBoyutIndex];
                farkliBoyutIndex = (farkliBoyutIndex + 1) % farkliBoyutlar.Length;

                Point agacKonumu;
                do
                {
                    agacKonumu = RastgeleKonumAl(agacBoyutu, agacBoyutu);
                } while (SandıkEngelleniyorMu(agacKonumu));

                PictureBox agacPictureBox = new PictureBox
                {
                    Size = new Size(hucreBoyutu * agacBoyutu, hucreBoyutu * agacBoyutu),
                    Location = new Point(agacKonumu.X * hucreBoyutu, agacKonumu.Y * hucreBoyutu),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "Agac"
                };

                if (agacKonumu.X < kenarBoyutu / 2)
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Agac", "KisAgac", kısagaclarResimYolu);
                    agacPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }
                else
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Agac", "YazAgac", yazagaclarResimYolu);
                    agacPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }

                panel.Controls.Add(agacPictureBox);
                agacPictureBox.BringToFront();
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
