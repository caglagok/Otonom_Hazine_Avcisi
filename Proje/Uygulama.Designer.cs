namespace prolab1
{
    partial class Uygulama
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            listBox1 = new ListBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 134);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(156, 27);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Viner Hand ITC", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(12, 212);
            button1.Name = "button1";
            button1.Size = new Size(156, 74);
            button1.TabIndex = 1;
            button1.Text = "Yeni Harita Oluştur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Location = new Point(180, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(975, 960);
            panel1.TabIndex = 2;
            // 
            // button2
            // 
            button2.Font = new Font("Viner Hand ITC", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(12, 331);
            button2.Name = "button2";
            button2.Size = new Size(156, 62);
            button2.TabIndex = 3;
            button2.Text = "Başlat";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Viner Hand ITC", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(32, 30);
            label1.Name = "label1";
            label1.Size = new Size(102, 31);
            label1.TabIndex = 4;
            label1.Text = "HEY SEN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Viner Hand ITC", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(162, 29);
            label2.TabIndex = 5;
            label2.Text = "Harita boyutu gir";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new Point(1178, 66);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(338, 564);
            listBox1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Viner Hand ITC", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(1190, 21);
            label3.Name = "label3";
            label3.Size = new Size(283, 29);
            label3.TabIndex = 7;
            label3.Text = "İşte Sandıklar ve Konumları";
            // 
            // Uygulama
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveCaption;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1528, 967);
            Controls.Add(label3);
            Controls.Add(listBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "Uygulama";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Oyun";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Panel panel1;
        private Button button2;
        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private Label label3;
    }
}