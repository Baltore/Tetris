using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Game;

namespace Tetris.UI
{
    public class GameUI : Form
    {
        private const int CellSize = 30; // Taille de chaque cellule
        private const int Rows = 20;    // Nombre de lignes
        private const int Columns = 10; // Nombre de colonnes

        private GameManager gameManager;

        private Label scoreLabel;
        private Label timeLabel;

        public GameUI()
        {
            // Configuration de la fenêtre
            this.Text = "Tetris";
            this.ClientSize = new Size(Columns * CellSize + 250, Rows * CellSize + 40);
            this.DoubleBuffered = true; // Active le double buffering pour éviter le clignotement

            // Initialisation des composants du jeu
            InitializeGameComponents();

            // Création des labels pour le score et le temps
            InitializeLabels();

            // Gestion des touches pour les interactions
            this.KeyDown += HandleKeyPress;

            // Timer pour rafraîchir l'interface graphique
            InitializeRenderTimer();
        }

        private void InitializeGameComponents()
        {
            // Initialisation du GameManager
            gameManager = new GameManager(Rows, Columns);
            gameManager.OnScoreUpdated += UpdateScore;
            gameManager.OnTimeUpdated += UpdateTime;
            gameManager.OnGameOver += HandleGameOver;
        }

        private void InitializeLabels()
        {
            // Label pour le score
            scoreLabel = new Label
            {
                Text = "Score: 0",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(200, 30),
                Location = new Point(Columns * CellSize + 40, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(scoreLabel);

            // Label pour le temps
            timeLabel = new Label
            {
                Text = "Time: 0s",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(200, 30),
                Location = new Point(Columns * CellSize + 40, 60),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(timeLabel);
        }

        private void InitializeRenderTimer()
        {
            Timer renderTimer = new Timer
            {
                Interval = 16 // ~60 FPS
            };
            renderTimer.Tick += (s, e) =>
            {
                this.Invalidate(); // Redessine l'interface
            };
            renderTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGrid(e.Graphics);
            DrawShape(e.Graphics, gameManager.GetGrid(), gameManager.GetNextShape());
        }

        private void DrawGrid(Graphics graphics)
        {
            Grid grid = gameManager.GetGrid();

            // Remplir le fond de la grille avec une couleur bleue
            graphics.Clear(Color.Blue);

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Color cellColor = grid.CellColors[row, col];
                    Brush brush = cellColor == Color.Black ? Brushes.Black : new SolidBrush(cellColor);

                    // Dessiner chaque cellule
                    graphics.FillRectangle(brush, (col * CellSize) + 20, (row * CellSize) + 20, CellSize, CellSize);
                    graphics.DrawRectangle(Pens.White, (col * CellSize) + 20, (row * CellSize) + 20, CellSize, CellSize);
                }
            }
        }

        private void DrawShape(Graphics graphics, Grid grid, Shape nextShape)
        {
            Shape currentShape = grid.CurrentShape;

            // Dessiner la forme actuelle
            if (currentShape != null)
            {
                for (int row = 0; row < currentShape.Matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < currentShape.Matrix.GetLength(1); col++)
                    {
                        if (currentShape.Matrix[row, col] == 1)
                        {
                            int x = (currentShape.Column + col) * CellSize;
                            int y = (currentShape.Row + row) * CellSize;
                            Brush brush = new SolidBrush(currentShape.Color);

                            graphics.FillRectangle(brush, x + 20, y + 20, CellSize, CellSize);
                            graphics.DrawRectangle(Pens.White, x + 20, y + 20, CellSize, CellSize);
                        }
                    }
                }
            }

            // Dessiner la prochaine forme
            if (nextShape != null)
            {
                int startX = Columns * CellSize + 20;
                int startY = 100;
                for (int row = 0; row < nextShape.Matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < nextShape.Matrix.GetLength(1); col++)
                    {
                        if (nextShape.Matrix[row, col] == 1)
                        {
                            int x = startX + col * CellSize;
                            int y = startY + row * CellSize;
                            Brush brush = new SolidBrush(nextShape.Color);

                            graphics.FillRectangle(brush, x + 20, y + 20, CellSize, CellSize);
                            graphics.DrawRectangle(Pens.White, x + 20, y + 20, CellSize, CellSize);
                        }
                    }
                }
            }
        }

        private void UpdateScore(int newScore)
        {
            scoreLabel.Text = $"Score: {newScore}";
        }

        private void UpdateTime(int elapsedSeconds)
        {
            timeLabel.Text = $"Time: {elapsedSeconds}s";
        }

        private void HandleGameOver()
        {
            MessageBox.Show("Game Over! Thanks for playing.", "Game Over");
            this.Close();
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            // Gestion des touches pour déplacer ou tourner les pièces
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gameManager.MoveShapeLeft();
                    break;
                case Keys.Right:
                    gameManager.MoveShapeRight();
                    break;
                case Keys.Up:
                    gameManager.RotateShape();
                    break;
                case Keys.Down:
                    gameManager.MoveShapeDown();
                    break;
            }

            // Redessiner immédiatement après un mouvement
            this.Invalidate();
        }
    }
}
