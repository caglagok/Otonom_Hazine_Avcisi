using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace prolab1.Core
{
    public class Karakter
    {
        public Karakter(string name, Point location)
        {
            Name = name;
            Location = location;
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }

        public void KarakterAtama(int hucreBoyutu, Panel panel)
        {
            string karakterResimYolu = Path.Combine(Application.StartupPath, "Asset", "Karakter", "karakter.jpeg");

            PictureBox karakterPictureBox = new PictureBox
            {
                Size = new Size(hucreBoyutu, hucreBoyutu),
                Location = new Point(Location.X * hucreBoyutu, Location.Y * hucreBoyutu),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(karakterResimYolu) ?? throw new FileNotFoundException("Image file not found")
            };

            panel.Controls.Add(karakterPictureBox);
        }
    }
}
