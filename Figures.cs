using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    static class Figures
    {
        static List<Block[,]> figures = constructFigures();

        static List<Block[,]> constructFigures()
        {
            List<Block[,]> result = new List<Block[,]>();
            Block[,] figure;

            // OO
            // O
            // O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(2, 1), new Point(3, 1), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);

            // OO
            //  O
            //  O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(2, 1), new Point(1, 1), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);

            // O
            // O
            // O
            // O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(2, 1), new Point(2, 0), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);

            // O
            // OO
            // O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(2, 1), new Point(3, 2), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);

            // OO
            // OO
            figure = constructFigure(new Vector2(4, 4), new List<Point> { 
                new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) });
            result.Add(figure);

            // O
            // OO
            //  O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(1, 2), new Point(1, 1), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);

            //  O
            // OO
            // O
            figure = constructFigure(new Vector2(5, 5), new List<Point> { 
                new Point(3, 1), new Point(3, 2), new Point(2, 2), new Point(2, 3) });
            result.Add(figure);
            return result;
        }

        static Block[,] constructFigure(Vector2 size, List<Point> plots)
        {
            Block[,] result = Block.getBlinkArray(size);

            foreach (var plot in plots)
                result[plot.X, plot.Y] = new Block(0);

            return result;
        }

        public static Block[,] getFigure()
        {
            int numb = Game1.Rand.Next(figures.Count());
            Block[,] result = (Block[,])(figures[numb].Clone());

            int newHue = Game1.Rand.Next(360);

            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < result.GetLength(1); j++)
                    if (result[i, j].isFill)
                        result[i, j] = new Block(newHue);

            return result;
        }
    }
}
