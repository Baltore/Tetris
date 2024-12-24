using System.Drawing;

namespace Tetris.Game
{
    public static class ShapeFactory
    {
        public static Shape CreateLine()
        {
            return new Shape(new int[,]
            {
                { 1, 1, 1, 1 }
            }, Color.Cyan);
        }

        public static Shape CreateSquare()
        {
            return new Shape(new int[,]
            {
                { 1, 1 },
                { 1, 1 }
            }, Color.Yellow);
        }

        public static Shape CreateT()
        {
            return new Shape(new int[,]
            {
                { 0, 1, 0 },
                { 1, 1, 1 }
            }, Color.Purple);
        }

        public static Shape CreateL()
        {
            return new Shape(new int[,]
            {
                { 1, 0, 0 },
                { 1, 1, 1 }
            }, Color.Orange);
        }

        public static Shape CreateReverseL()
        {
            return new Shape(new int[,]
            {
                { 0, 0, 1 },
                { 1, 1, 1 }
            }, Color.Brown);
        }

        public static Shape CreateZ()
        {
            return new Shape(new int[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 }
            }, Color.Red);
        }

        public static Shape CreateReverseZ()
        {
            return new Shape(new int[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 }
            }, Color.Green);
        }
    }
}
