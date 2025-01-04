using System;
using System.Drawing;

namespace Tetris.Game
{
    public class Grid
    {
        public int[,] Cells { get; private set; } // Tableau 2D représentant l'état de la grille
        public Color[,] CellColors { get; private set; } // Tableau pour les couleurs des cellules
        public Shape CurrentShape { get; set; }   // La forme actuellement en jeu
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Cells = new int[rows, columns];
            CellColors = new Color[rows, columns];
        }

        // Affiche l'état de la grille (pour debug)
        public void PrintGrid()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(Cells[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Vérifie si une forme peut être déplacée
        public bool CanMove(Shape shape, int newRow, int newColumn)
        {
            for (int row = 0; row < shape.Height; row++)
            {
                for (int col = 0; col < shape.Width; col++)
                {
                    if (shape.Matrix[row, col] == 1)
                    {
                        int gridRow = newRow + row;
                        int gridCol = newColumn + col;

                        // Vérifie les limites et si la cellule est occupée
                        if (gridRow < 0 || gridRow >= Rows || gridCol < 0 || gridCol >= Columns || Cells[gridRow, gridCol] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CanRotate(Shape shape)
        {
            int rows = shape.Matrix.GetLength(0);
            int columns = shape.Matrix.GetLength(1);
            int[,] rotatedMatrix = new int[columns, rows];

            // Calcul de la matrice tournée (sans la modifier dans `shape`)
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    rotatedMatrix[j, rows - 1 - i] = shape.Matrix[i, j];
                }
            }

            // Vérifie si la pièce tournée reste valide
            for (int row = 0; row < rotatedMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < rotatedMatrix.GetLength(1); col++)
                {
                    if (rotatedMatrix[row, col] == 1)
                    {
                        int gridRow = shape.Row + row;
                        int gridCol = shape.Column + col;

                        // Vérifie si la rotation est en dehors des limites ou en collision
                        if (gridRow < 0 || gridRow >= Rows || gridCol < 0 || gridCol >= Columns || Cells[gridRow, gridCol] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        // Ajoute une forme à la grille
        public void AddShape(int[,] shapeMatrix, int startRow, int startColumn, Color shapeColor)
        {
            for (int i = 0; i < shapeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < shapeMatrix.GetLength(1); j++)
                {
                    if (shapeMatrix[i, j] != 0)
                    {
                        int gridRow = startRow + i;
                        int gridCol = startColumn + j;

                        // Met à jour la cellule avec l'indice et la couleur de la forme
                        Cells[gridRow, gridCol] = 1; // Indique qu'une pièce occupe cette cellule
                        CellColors[gridRow, gridCol] = shapeColor;
                    }
                }
            }
        }


        // Supprime les lignes complètes et renvoie le nombre de lignes supprimées
        public int ClearFullLines()
        {
            int clearedLines = 0;
            for (int i = 0; i < Rows; i++)
            {
                bool isFull = true;
                for (int j = 0; j < Columns; j++)
                {
                    if (Cells[i, j] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                {
                    clearedLines++;
                    ClearLine(i);
                }
            }
            return clearedLines;
        }

        // Efface une ligne et déplace les lignes au-dessus vers le bas
        private void ClearLine(int row)
        {
            for (int i = row; i > 0; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cells[i, j] = Cells[i - 1, j];
                    CellColors[i, j] = CellColors[i - 1, j];
                }
            }

            // Remplir la première ligne de zéros et réinitialiser les couleurs
            for (int j = 0; j < Columns; j++)
            {
                Cells[0, j] = 0;
                CellColors[0, j] = Color.Blue;
            }
        }


        public void ClearShape(Shape shape)
        {
            for (int row = 0; row < shape.Height; row++)
            {
                for (int col = 0; col < shape.Width; col++)
                {
                    if (shape.Matrix[row, col] == 1)
                    {
                        int gridRow = shape.Row + row;
                        int gridCol = shape.Column + col;

                        // Efface uniquement si la cellule appartient à la pièce actuelle
                        if (gridRow >= 0 && gridRow < Rows && gridCol >= 0 && gridCol < Columns)
                        {
                            Cells[gridRow, gridCol] = 0;
                            CellColors[gridRow, gridCol] = Color.Blue;
                        }
                    }
                }
            }
        }


        // Méthode pour obtenir la forme actuelle
        public Shape GetCurrentShape()
        {
            return CurrentShape;
        }

        // Méthode qui met à jour la grille avec la forme
        public void UpdateGridState(Shape shape)
        {
            AddShape(shape.Matrix, shape.Row, shape.Column, shape.Color); // Utilise la couleur associée à la forme
        }
        public void Reset()
        {
            // Réinitialise la grille
            Cells = new int[Rows, Columns];
        }
    }
}
