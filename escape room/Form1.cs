using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;

namespace escape_room
{
    public partial class Form1 : Form
    {
        int rows = 25;
        int cols = 25;

        Panel[,] tiles;

        int tileSize = 20;

        Random rnd = new Random();

        // Player position
        int playerRow;
        int playerCol;

        // Question wall position
        int questionRow;
        int questionCol;

        // Check if solved
        bool questionSolved = false;

        int finishRow;
        int finishCol;
        public Form1()
        {
            InitializeComponent();

            tiles = new Panel[rows, cols];

            // Form settings
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;

            GenerateMaze();

            // Keyboard event
            this.KeyDown += Form1_KeyDown;
        }

        void GenerateMaze()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Panel p = new Panel();

                    p.Width = tileSize;
                    p.Height = tileSize;

                    // Spacing
                    p.Left = col * tileSize;
                    p.Top = row * tileSize;

                    // No border
                    p.BorderStyle = BorderStyle.None;

                    int chance = rnd.Next(100);

                    // Start and finish always open
                    if ((row == 0 && col == 0) ||
                        (row == rows - 1 && col == cols - 1))
                    {
                        p.BackColor = Color.Black;
                    }
                    // Walls
                    else if (chance < 30)
                    {
                        p.BackColor = Color.Cyan;
                    }
                    // Paths
                    else
                    {
                        p.BackColor = Color.Black;
                    }

                    tiles[row, col] = p;

                    this.Controls.Add(p);
                }
            }

            // Player
            while (true)
            {
                int randomRow = rnd.Next(rows);
                int randomCol = rnd.Next(cols);

                // Must be path
                // Must not be player spawn
                if (tiles[randomRow, randomCol].BackColor == Color.Black &&
                    (randomRow != playerRow || randomCol != playerCol))
                {
                    finishRow = randomRow;
                    finishCol = randomCol;

                    break;
                }
            }

            // Put question wall beside finish
            questionRow = finishRow;
            questionCol = finishCol - 1;

            // Safety check
            if (questionCol < 0)
            {
                questionCol = finishCol + 1;
            }

            // Draw finish
            tiles[finishRow, finishCol].BackColor = Color.Gold;

            // Draw question wall
            tiles[questionRow, questionCol].BackColor = Color.Magenta;

            // Draw player
            tiles[playerRow, playerCol].BackColor = Color.Lime;
        }

        void ResetGame()
        {
            // Remove old tiles
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    this.Controls.Remove(tiles[row, col]);
                }
            }

            // Reset question state
            questionSolved = false;

            // Create new maze
            GenerateMaze();
        }

        void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            int newRow = playerRow;
            int newCol = playerCol;

            // Movement
            if (e.KeyCode == Keys.Up)
            {
                newRow--;
            }
            else if (e.KeyCode == Keys.Down)
            {
                newRow++;
            }
            else if (e.KeyCode == Keys.Left)
            {
                newCol--;
            }
            else if (e.KeyCode == Keys.Right)
            {
                newCol++;
            }

            // Prevent going outside map
            if (newRow < 0 || newRow >= rows ||
                newCol < 0 || newCol >= cols)
            {
                return;
            }

            // Prevent walking through walls
            if (tiles[newRow, newCol].BackColor == Color.Cyan)
            {
                return;
            }

            // Question wall
            if (newRow == questionRow &&
                newCol == questionCol &&
                questionSolved == false)
            {
                AskQuestion();
                return;
            }

            // Remove old player tile
            tiles[playerRow, playerCol].BackColor = Color.Black;

            // Move player
            playerRow = newRow;
            playerCol = newCol;

            // Win condition
            if (playerRow == finishRow &&
                playerCol == finishCol)
            {
                MessageBox.Show("YOU ESCAPED!");

                Application.DoEvents();

                System.Threading.Thread.Sleep(1000);

                ResetGame();
            }

            // Draw player
            tiles[playerRow, playerCol].BackColor = Color.Lime;

            // Keep finish tile gold
            tiles[finishRow, finishCol].BackColor = Color.Gold;
        }

        void AskQuestion()
        {
            QuestionForm qf = new QuestionForm();

            qf.ShowDialog();

            if (qf.isCorrect)
            {
                questionSolved = true;

                tiles[questionRow, questionCol].BackColor = Color.Black;
            }
        }
    }
}
