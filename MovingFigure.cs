using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ColorConversion;

namespace Tetris
{
    class MovingFigure
    {
        public Block[,] figure { get; private set; }
        Vector2 position;
        public Vector2 Position { get { return position; }}

        public MovingFigure()
        {
        }

        public void setFigure(Block[,] figure)
        {
            this.figure = figure;

            position = new Vector2((int)((Constants.FIELDBLOCKZISE.X - figure.GetLength(0)) / 2), -figure.GetLength(1));
        }

        public void MoveLeft()
        {
            position.X--;
        }

        public void MoveRight()
        {
            position.X++;
        }

        public void MoveDown()
        {
            position.Y++;
        }

        public void Turn()
        {
            figure = getTurned();
        }

        public Block[,] getTurned()
        {
            Point[,] positions = new Point[figure.GetLength(0), figure.GetLength(1)];
            int offset = (int)Math.Floor((float)figure.GetLength(0) / 2);

            for (int i = 0; i < figure.GetLength(0); i++)
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    positions[i, j] = new Point(i, j);

                    positions[i, j].X -= offset;
                    positions[i, j].Y -= offset;
                    
                    if ((figure.GetLength(0) % 2) == 0)
                    {
                        if (positions[i, j].X >= 0)
                            positions[i, j].X++;
                        if (positions[i, j].Y >= 0)
                            positions[i, j].Y++;
                    }

                    positions[i, j] = new Point(positions[i, j].Y, -positions[i, j].X);

                    positions[i, j].X += offset;
                    positions[i, j].Y += offset;

                    if ((figure.GetLength(0) % 2) == 0)
                    {
                        if (positions[i, j].X >= offset)
                            positions[i, j].X--;
                        if (positions[i, j].Y >= offset)
                            positions[i, j].Y--;
                    }
                }

            Block[,] newFigure = new Block[figure.GetLength(0), figure.GetLength(1)];

            for (int i = 0; i < figure.GetLength(0); i++)
                for (int j = 0; j < figure.GetLength(1); j++)
                    newFigure[i, j] = figure[positions[i, j].X, positions[i, j].Y];

            return newFigure;
        }
    }
}
