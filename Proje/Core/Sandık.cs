using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prolab1.Core
{
    public class Sandık
    {
        private Random random = new Random();
        private List<string> sandikResimler;
        private int hucreBoyutu;
        private Panel panel;
        public Sandık(int hucreBoyutu, Panel panel)
        {
            this.hucreBoyutu = hucreBoyutu;
            this.panel = panel;

        }

        public void SandıkAtama()
        {
            sandikResimler = new List<string>
            {
                "altınsandık.jpeg",
                "gumussandık.jpeg",
                "zümrütsandık.jpeg",
                "bakırsandık.jpeg",
            };

            for (int i = 0; i < 22; i++)
            {
                string sandikResimYolu = sandikResimler[i % sandikResimler.Count];
                string fullPath = Path.Combine(Application.StartupPath, "Asset", "Sandik", sandikResimYolu);

                Point sandikKonumu;
                do
                {
                    sandikKonumu = RastgeleKonumAl();
                } while (SandıkEngelleniyorMu(sandikKonumu));

                PictureBox sandikPictureBox = new PictureBox
                {
                    Size = new Size(hucreBoyutu, hucreBoyutu),
                    Location = new Point(sandikKonumu.X * hucreBoyutu, sandikKonumu.Y * hucreBoyutu),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Image.FromFile(fullPath) ?? throw new FileNotFoundException("Image file not found"),
                    Tag = "Sandik"
                };
                panel.Controls.Add(sandikPictureBox);
            }
        }

        private Point RastgeleKonumAl()
        {
            int randomX = random.Next(panel.Width / hucreBoyutu);
            int randomY = random.Next(panel.Height / hucreBoyutu);

            return new Point(randomX, randomY);
        }
        private bool SandıkEngelleniyorMu(Point chestLocation)
        {
            return panel.Controls.OfType<PictureBox>().Any(pictureBox =>
            {
                int pictureBoxX = pictureBox.Location.X / hucreBoyutu;
                int pictureBoxY = pictureBox.Location.Y / hucreBoyutu;

                if (pictureBox.Tag != null && pictureBox.Tag.ToString() == "Sandik")
                {
                    return false; 
                }

                return Math.Abs(pictureBoxX - chestLocation.X) <= 1 && Math.Abs(pictureBoxY - chestLocation.Y) <= 1;
            });
        }

    }
}