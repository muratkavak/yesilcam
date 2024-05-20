namespace YesilCam
{
    partial class Fonksiyon1
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
            oyuncuListBox = new ListBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // oyuncuListBox
            // 
            oyuncuListBox.FormattingEnabled = true;
            oyuncuListBox.ItemHeight = 15;
            oyuncuListBox.Location = new Point(12, 34);
            oyuncuListBox.Name = "oyuncuListBox";
            oyuncuListBox.Size = new Size(164, 184);
            oyuncuListBox.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(194, 182);
            button1.Name = "button1";
            button1.Size = new Size(104, 36);
            button1.TabIndex = 1;
            button1.Text = "Sonucu Göster";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Fonksiyon1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(oyuncuListBox);
            Name = "Fonksiyon1";
            Text = "Fonksiyon1";
            ResumeLayout(false);
        }

        #endregion

        private ListBox oyuncuListBox;
        private Button button1;
    }
}