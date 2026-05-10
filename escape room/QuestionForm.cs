using System;
using System.IO;
using System.Windows.Forms;

namespace escape_room
{
    public partial class QuestionForm : Form
    {
        // Check if answer is correct
        public bool isCorrect = false;

        // Store correct answer
        string correctAnswer = "";

        // Random question picker
        Random rnd = new Random();

        public QuestionForm()
        {
            InitializeComponent();

            // Load event
            this.Load += QuestionForm_Load;
        }

        private void QuestionForm_Load(object? sender, EventArgs e)
        {
            // Read all questions from txt file
            string[] lines = File.ReadAllLines("questions.txt");

            // Pick random question
            int randomIndex = rnd.Next(lines.Length);

            string selectedQuestion = lines[randomIndex];

            // Split question and answer
            string[] parts = selectedQuestion.Split('|');

            // Show question
            labelQuestion.Text = parts[0];

            // Save correct answer
            correctAnswer = parts[1];
        }

        private void btnSubmit_Click(object? sender, EventArgs e)
        {
            // Check answer
            if (txtAnswer.Text.ToLower() == correctAnswer.ToLower())
            {
                isCorrect = true;

                MessageBox.Show("Correct!");

                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Answer!");
            }
        }
    }
}