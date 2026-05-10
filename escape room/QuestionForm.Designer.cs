namespace escape_room
{
    partial class QuestionForm
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
            labelQuestion = new Label();
            txtAnswer = new TextBox();
            btnSubmit = new Button();
            SuspendLayout();
            // 
            // labelQuestion
            // 
            labelQuestion.AutoSize = true;
            labelQuestion.Location = new Point(330, 53);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(38, 15);
            labelQuestion.TabIndex = 0;
            labelQuestion.Text = "label1";
            // 
            // txtAnswer
            // 
            txtAnswer.Location = new Point(302, 146);
            txtAnswer.Name = "txtAnswer";
            txtAnswer.Size = new Size(100, 23);
            txtAnswer.TabIndex = 1;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(315, 241);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "button1";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // QuestionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 400);
            Controls.Add(btnSubmit);
            Controls.Add(txtAnswer);
            Controls.Add(labelQuestion);
            Name = "QuestionsForm";
            Text = "QuestionsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelQuestion;
        private TextBox txtAnswer;
        private Button btnSubmit;
    }
}