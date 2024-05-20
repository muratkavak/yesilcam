namespace YesilCam
{
    partial class Fonksiyon3
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
            yonetmenListBox = new ListBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // yonetmenListBox
            // 
            yonetmenListBox.FormattingEnabled = true;
            yonetmenListBox.ItemHeight = 15;
            yonetmenListBox.Location = new Point(26, 23);
            yonetmenListBox.Name = "yonetmenListBox";
            yonetmenListBox.Size = new Size(137, 184);
            yonetmenListBox.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(181, 165);
            button1.Name = "button1";
            button1.Size = new Size(100, 41);
            button1.TabIndex = 1;
            button1.Text = "Sonucu Göster";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Fonksiyon2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(yonetmenListBox);
            Name = "Fonksiyon2";
            Text = "Fonksiyon2";
            ResumeLayout(false);
        }

        #endregion

        private ListBox yonetmenListBox;
        private Button button1;
    }
}