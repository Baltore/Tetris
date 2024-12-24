using System;
using System.Drawing;

namespace Tetris.Game
{
    public class Shape
    {
        public int[,] Matrix { get; private set; } // Matrice qui représente la forme
        public Color Color { get; private set; } // Couleur de la forme
        public int Row { get; set; } // Position actuelle (ligne)
        public int Column { get; set; } // Position actuelle (colonne)
        public int ColorIndex { get; private set; } // Indice de la couleur (utilisé pour la grille)

        // Constructeur
        public Shape(int[,] matrix, Color color)
        {
            Matrix = matrix;
            Color = color;
            Row = 0; // Commence en haut de la grille
            Column = 0; // Centré par défaut
            ColorIndex = color.ToArgb(); // Conversion de la couleur en indice entier
        }

        // Effectue une rotation de la forme (dans le sens horaire)
        public void Rotate()
        {
            int rows = Matrix.GetLength(0);
            int columns = Matrix.GetLength(1);
            int[,] rotatedMatrix = new int[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    rotatedMatrix[j, rows - 1 - i] = Matrix[i, j];
                }
            }

            Matrix = rotatedMatrix;
        }


        // Retourne la largeur de la forme
        public int Width => Matrix.GetLength(1);

        // Retourne la hauteur de la forme
        public int Height => Matrix.GetLength(0);
    }
}
