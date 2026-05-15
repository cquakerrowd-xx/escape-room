using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace escape_room
{
    public partial class Form1 : Form
    {
        // Maze size
        int rows = 25;
        int cols = 25;

        // Tile settings
        int tileSize = 20;

        // Maze tiles
        Panel[,] tiles;

        // Random generator
        Random rnd = new Random();

        Button btnReset = new Button();
        Button btnBack = new Button();

        // Player position
        int playerRow;
        int playerCol;

        // Finish position
        int finishRow;
        int finishCol;

        // Player keys
        int keys = 0;

        // Required keys to win
        int requiredKeys = 3;

        // Multiple question doors
        List<(int row, int col, bool solved)> questionDoors = new();

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
            CreateButtons();

            // Keyboard movement
            this.KeyDown += Form1_KeyDown;
        }

        void GenerateMaze()
        {

            // Clear old doors
            questionDoors.Clear();

            // Create maze
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Panel p = new Panel();

                    p.Width = tileSize;
                    p.Height = tileSize;

                    // No spaces between tiles
                    p.Left = col * tileSize;
                    p.Top = row * tileSize;

                    p.BorderStyle = BorderStyle.None;

                    int chance = rnd.Next(100);

                    // Wall chance
                    if (chance < 30)
                    {
                        p.BackColor = Color.Cyan;
                    }
                    else
                    {
                        p.BackColor = Color.Black;
                    }

                    tiles[row, col] = p;

                    this.Controls.Add(p);
                }
            }

            // RANDOM PLAYER SPAWN
            while (true)
            {
                int r = rnd.Next(rows);
                int c = rnd.Next(cols);

                if (tiles[r, c].BackColor == Color.Black)
                {
                    playerRow = r;
                    playerCol = c;
                    break;
                }
            }

            // RANDOM FINISH SPAWN
            while (true)
            {
                int r = rnd.Next(rows);
                int c = rnd.Next(cols);

                if (tiles[r, c].BackColor == Color.Black &&
                    (r != playerRow || c != playerCol))
                {
                    finishRow = r;
                    finishCol = c;
                    break;
                }
            }

            // DRAW FINISH
            tiles[finishRow, finishCol].BackColor = Color.Gold;

            // CREATE MULTIPLE QUESTION DOORS
            int doorCount = 3;

            for (int i = 0; i < doorCount; i++)
            {
                while (true)
                {
                    int r = rnd.Next(rows);
                    int c = rnd.Next(cols);

                    // Must be path
                    // Must not be player
                    // Must not be finish
                    if (tiles[r, c].BackColor == Color.Black &&
                        (r != playerRow || c != playerCol) &&
                        (r != finishRow || c != finishCol))
                    {
                        // Make magenta door
                        tiles[r, c].BackColor = Color.Magenta;

                        // Save door
                        questionDoors.Add((r, c, false));

                        break;
                    }
                }
            }

            // DRAW PLAYER
            tiles[playerRow, playerCol].BackColor = Color.Lime;

            this.Text = "Keys: " + keys + " / " + requiredKeys;
        }

        void CreateButtons()
        {
            btnReset.FlatStyle = FlatStyle.Flat;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnReset.ForeColor = Color.Black;
            btnBack.ForeColor = Color.White;
            // RESET BUTTON
            btnReset.Text = "Reset";
            btnReset.Width = 80;
            btnReset.Height = 30;

            btnReset.Left = 520;
            btnReset.Top = 20;

            btnReset.BackColor = Color.Cyan;

            btnReset.Click += BtnReset_Click;

            this.Controls.Add(btnReset);

            // BACK BUTTON
            btnBack.Text = "Back";
            btnBack.Width = 80;
            btnBack.Height = 30;

            btnBack.Left = 520;
            btnBack.Top = 60;

            btnBack.BackColor = Color.Magenta;

            btnBack.Click += BtnBack_Click;

            this.Controls.Add(btnBack);
        }

        void BtnReset_Click(object? sender, EventArgs e)
        {
            keys = 0;

            ResetGame();
        }

        void BtnBack_Click(object? sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();

            menu.Show();

            this.Close();
        }

        void ResetGame()
        {
            // Remove old maze
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    this.Controls.Remove(tiles[row, col]);
                }
            }

            // Generate new maze
            GenerateMaze();
        }

        void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            int newRow = playerRow;
            int newCol = playerCol;

            // MOVEMENT
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

            // OUTSIDE MAP
            if (newRow < 0 || newRow >= rows ||
                newCol < 0 || newCol >= cols)
            {
                return;
            }

            // CYAN WALLS
            if (tiles[newRow, newCol].BackColor == Color.Cyan)
            {
                return;
            }

            foreach (var door in questionDoors)
            {
                if (newRow == door.row &&
                    newCol == door.col &&
                    door.solved == false)
                {
                    AskQuestion(door.row, door.col);

                    return;
                }
            }

            // REMOVE OLD PLAYER
            tiles[playerRow, playerCol].BackColor = Color.Black;

            // KEEP OPENED DOORS BLACK
            foreach (var door in questionDoors)
            {
                if (door.solved)
                {
                    tiles[door.row, door.col].BackColor = Color.Black;
                }
            }

            // MOVE PLAYER
            playerRow = newRow;
            playerCol = newCol;

            // Finish locked
            if (newRow == finishRow &&
                newCol == finishCol)
            {
                if (keys < requiredKeys)
                {
                    MessageBox.Show("You need 3 keys!");

                    return;
                }
            }

            if (playerRow == finishRow &&
                playerCol == finishCol)
            {
                MessageBox.Show("YOU ESCAPED!");

                Application.DoEvents();

                Thread.Sleep(1000);

                keys = 0;

                ResetGame();

                return;
            }

            // DRAW PLAYER
            tiles[playerRow, playerCol].BackColor = Color.Lime;

            // KEEP FINISH GOLD
            tiles[finishRow, finishCol].BackColor = Color.Gold;

            // KEEP UNSOLVED DOORS MAGENTA
            foreach (var door in questionDoors)
            {
                if (!door.solved)
                {
                    tiles[door.row, door.col].BackColor = Color.Magenta;
                }
            }
        }

        void AskQuestion(int r, int c)
        {
            QuestionForm qf = new QuestionForm();

            qf.ShowDialog();

            // Correct answer
            if (qf.isCorrect)
            {
                // Update solved door
                for (int i = 0; i < questionDoors.Count; i++)
                {
                    if (questionDoors[i].row == r &&
                        questionDoors[i].col == c)
                    {
                        questionDoors[i] = (r, c, true);

                        break;
                    }
                }

                // Remove door color
                tiles[r, c].BackColor = Color.Black;

                // Give player key
                keys++;

                // Update title
                this.Text = "Keys: " + keys + " / " + requiredKeys;

                MessageBox.Show("You got a key!");
            }
        }
    }
}
