using System;
using System.Drawing;
using System.Timers;
using Tetris.Game;

namespace Tetris.Game
{
    public class GameManager
    {
        private Grid grid; // La grille où les formes sont jouées
        private Shape currentShape; // La forme actuellement en jeu
        private Shape nextShape; // La prochaine forme à afficher
        private Timer gameTimer; // Timer pour mettre à jour le jeu
        private Random random; // Générateur de formes aléatoires

        private ScoreManager scoreManager; // Gestion du score
        private TimerManager timerManager; // Gestion du temps

        public event Action<int> OnScoreUpdated; // Événement pour mettre à jour l'UI du score
        public event Action<int> OnTimeUpdated;  // Événement pour mettre à jour l'UI du temps
        public event Action OnGameOver;         // Événement pour signaler la fin du jeu

        public GameManager(int rows, int columns)
        {
            grid = new Grid(rows, columns);
            random = new Random();

            scoreManager = new ScoreManager();
            timerManager = new TimerManager();

            // Abonne les événements
            scoreManager.OnScoreUpdated += (score) => OnScoreUpdated?.Invoke(score);
            timerManager.OnTimeUpdated += (time) => OnTimeUpdated?.Invoke(time);
            timerManager.OnThresholdReached += AdjustGameSpeed;

            // Initialiser les formes
            currentShape = GenerateRandomShape();
            nextShape = GenerateRandomShape();

            // Placer la forme initiale au centre de la grille
            CenterShape(currentShape);

            if (!grid.CanMove(currentShape, currentShape.Row, currentShape.Column))
            {
                // Si la pièce initiale ne peut pas être placée, c'est un Game Over immédiat
                OnGameOver?.Invoke();
                return;
            }

            // Initialiser le timer de jeu
            gameTimer = new Timer(500); // Intervalle de 500 ms
            gameTimer.Elapsed += UpdateGameState;
            gameTimer.Start();

            // Démarrer le TimerManager pour le suivi du temps
            timerManager.Start();
        }


        private void CenterShape(Shape shape)
        {
            shape.Row = 0; // Commence en haut de la grille
            shape.Column = (grid.Columns - shape.Width) / 2; // Centré horizontalement
        }

        private Shape GenerateRandomShape()
        {
            int shapeIndex = random.Next(7); // 7 formes différentes
            return shapeIndex switch
            {
                0 => ShapeFactory.CreateLine(),
                1 => ShapeFactory.CreateSquare(),
                2 => ShapeFactory.CreateT(),
                3 => ShapeFactory.CreateL(),
                4 => ShapeFactory.CreateReverseL(),
                5 => ShapeFactory.CreateZ(),
                6 => ShapeFactory.CreateReverseZ(),
                _ => ShapeFactory.CreateSquare(), // Par défaut
            };
        }

        private void UpdateGameState(object sender, ElapsedEventArgs e)
        {
            MoveShapeDown();
            grid.PrintGrid(); // Ajout pour vérifier visuellement les mises à jour de la grille
        }
        
        private void AdjustGameSpeed(int threshold)
        {
            gameTimer.Interval = Math.Max(100, gameTimer.Interval - 100); // Réduit l'intervalle
        }

        public void MoveShapeLeft()
        {
            // Efface la pièce actuelle de la grille
            grid.ClearShape(currentShape);

            // Vérifie si la pièce peut se déplacer à gauche
            if (grid.CanMove(currentShape, currentShape.Row, currentShape.Column - 1))
            {
                currentShape.Column--; // Déplace la pièce à gauche
            }

            // Ajoute la pièce à la nouvelle position
            grid.AddShape(currentShape.Matrix, currentShape.Row, currentShape.Column, currentShape.Color);
        }


        public void MoveShapeRight()
        {
            // Efface la pièce actuelle de la grille
            grid.ClearShape(currentShape);

            // Vérifie si la pièce peut se déplacer à droite
            if (grid.CanMove(currentShape, currentShape.Row, currentShape.Column + 1))
            {
                currentShape.Column++; // Déplace la pièce à droite
            }

            // Ajoute la pièce à la nouvelle position
            grid.AddShape(currentShape.Matrix, currentShape.Row, currentShape.Column, currentShape.Color);
        }

        public void RotateShape()
        {
            // Efface temporairement la pièce actuelle de la grille
            grid.ClearShape(currentShape);

            // Vérifie si la rotation est possible
            if (grid.CanRotate(currentShape))
            {
                currentShape.Rotate();
            }

            // Réintègre la pièce dans la grille après la rotation (si elle a été effectuée)
            grid.AddShape(currentShape.Matrix, currentShape.Row, currentShape.Column, currentShape.Color);
        }

        // Ajout de la propriété isLocked
        public void MoveShapeDown()
        {
            // Efface la pièce actuelle de la grille
            grid.ClearShape(currentShape);

            // Vérifie si la pièce peut descendre
            if (grid.CanMove(currentShape, currentShape.Row + 1, currentShape.Column))
            {
                currentShape.Row++; // Descend d'une ligne
            }
            else
            {
                // Verrouille la pièce si elle ne peut plus descendre
                LockShapeInPlace();
                CheckForCompletedLines();
                currentShape = nextShape;
                nextShape = GenerateRandomShape();
                CenterShape(currentShape);

                // Vérifie si une nouvelle pièce peut spawn
                if (!grid.CanMove(currentShape, currentShape.Row, currentShape.Column))
                {
                    EndGame();
                }
            }

            // Met à jour la grille avec la nouvelle position de la pièce
            grid.AddShape(currentShape.Matrix, currentShape.Row, currentShape.Column, currentShape.Color);
        }


        private void LockShapeInPlace()
        {
            // Ajoute la pièce verrouillée dans la grille
            grid.AddShape(currentShape.Matrix, currentShape.Row, currentShape.Column, currentShape.Color);
        }

        private void CheckForCompletedLines()
        {
            int linesCleared = grid.ClearFullLines();
            if (linesCleared > 0)
            {
                scoreManager.AddPoints(linesCleared);

                // Accélérer le jeu
                gameTimer.Interval = Math.Max(100, gameTimer.Interval - 10);
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();
            timerManager.Pause();
            OnGameOver?.Invoke();
        }

        public int GetScore()
        {
            return scoreManager.Score; // Accède à la propriété Score de ScoreManager
        }

        public Grid GetGrid()
        {
            return grid;
        }

        public Shape GetNextShape()
        {
            return nextShape;
        }

        public void ResetGame()
        {
            gameTimer.Stop();
            grid.Reset();
            scoreManager.Reset();
            timerManager.Reset();

            // Réinitialiser les formes
            currentShape = GenerateRandomShape();
            nextShape = GenerateRandomShape();
            CenterShape(currentShape);

            gameTimer.Start();
            timerManager.Start();
        }
    }
}