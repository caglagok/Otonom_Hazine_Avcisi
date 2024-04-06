using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public class Duvarlar : HareketsizEngeller
    {
        private Random random = new Random();
        private List<string> duvarResimler;
        private int hucreBoyutu;
        private Panel panel;
        private int kenarBoyutu;
        public Duvarlar(int hucreBoyutu, Panel panel, int kenarBoyutu) : base(hucreBoyutu, panel, kenarBoyutu, "Duvarlar")
        {
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;
            this.kenarBoyutu = kenarBoyutu;
        }
        public void DuvarAtama()
        {
            duvarResimler = new List<string>
            {
               "duvar.jpeg"
            };

            for (int i = 0; i < 20; i++)
            {
                string duvarResimYolu = duvarResimler[i % duvarResimler.Count];

                Point duvarKonumu;
                do
                {
                    duvarKonumu = RastgeleKonumAl(10,1);
                } while (SandıkEngelleniyorMu(duvarKonumu));

                PictureBox duvarPictureBox = new PictureBox
                {
                    Size = new Size(hucreBoyutu * 10, hucreBoyutu),
                    Location = new Point(duvarKonumu.X * hucreBoyutu, duvarKonumu.Y * hucreBoyutu),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "Duvar"
                };
                

                string fullPath = Path.Combine(Application.StartupPath, "Asset", "Duvar", duvarResimYolu);
                duvarPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");

                panel.Controls.Add(duvarPictureBox);
                duvarPictureBox.BringToFront();
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