namespace escape_room
{
    partial class MainMenu
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
            btnStart = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(196, 174);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(154, 44);
            btnStart.TabIndex = 0;
            btnStart.Text = "PLAY";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += button1_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(196, 258);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(154, 44);
            btnExit.TabIndex = 1;
            btnExit.Text = "EXIT";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            Name = "MainMenu";
            Text = "MainMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button btnStart;
        private Button btnExit;
    }
}