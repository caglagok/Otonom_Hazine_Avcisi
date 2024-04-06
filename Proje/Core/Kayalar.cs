using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolab1.Core
{
    public class Kayalar : HareketsizEngeller
    {
        private Random random = new Random();
        private List<string> kıskayalıkResimler;
        private List<string> yazkayalıkResimler;
        private int hucreBoyutu;
        private Panel panel;
        private int kenarBoyutu;

        public Kayalar(int hucreBoyutu, Panel panel, int kenarBoyutu) : base(hucreBoyutu, panel, kenarBoyutu, "Kayalar")
        {
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;
            this.kenarBoyutu = kenarBoyutu;
        }


        public void KayalarAtama()
        {
            kıskayalıkResimler = new List<string>
            {
                "karlıbuyuktas.jpeg",
                "karlıortatas.jpeg",
                "karlıkucuktas.jpeg"
            };
            yazkayalıkResimler = new List<string>
            {
                "buyuktas.jpeg",
                "ortatas.jpeg",
                "kucuktas.png"
            };
            int[] farkliBoyutlar = new int[] { 2, 3};
            int farkliBoyutIndex = 0;

            for (int i = 0; i < 12; i++)
            {
                string kıskayalıkResimYolu = kıskayalıkResimler[i % kıskayalıkResimler.Count];
                string yazkayalıkResimYolu = yazkayalıkResimler[i % yazkayalıkResimler.Count];

                int kayaBoyutu = farkliBoyutlar[farkliBoyutIndex];
                farkliBoyutIndex = (farkliBoyutIndex + 1) % farkliBoyutlar.Length;

                Point kayaKonumu;
                do
                {
                    kayaKonumu = RastgeleKonumAl(kayaBoyutu, kayaBoyutu);
                } while (SandıkEngelleniyorMu(kayaKonumu));

                PictureBox kayaPictureBox = new PictureBox
                {
                    Size = new Size(hucreBoyutu * kayaBoyutu, hucreBoyutu * kayaBoyutu),
                    Location = new Point(kayaKonumu.X * hucreBoyutu, kayaKonumu.Y * hucreBoyutu),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "Kayalık"
                };

                if (kayaKonumu.X < kenarBoyutu / 2)
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Kayalık", "KısKayalık", kıskayalıkResimYolu);
                    kayaPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }
                else 
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Asset", "Kayalık", "YazKayalık", yazkayalıkResimYolu);
                    kayaPictureBox.Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found");
                }

                panel.Controls.Add(kayaPictureBox);
                kayaPictureBox.BringToFront();
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
