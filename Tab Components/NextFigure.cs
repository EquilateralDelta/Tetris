using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Tetris
{
    public class NextFigure
    {
        public Block[,] figure { get; private set; }
        Vector2 position;
        GameTab gameTab;

        Texture2D nextFigureTexture;

        public NextFigure(GameTab gameTab)
        {
            this.gameTab = gameTab;

            figure = Block.getBlinkArray(new Vector2(Constants.FIGUREMAXSIZE));
            setNewFigure();
            position = Constants.NEXTBLOCKPOSITION;
        }

        public void LoadContent()
        {
            nextFigureTexture = gameTab.Game.Content.Load<Texture2D>("preview");
        }

        public void Draw()
        {
            ((Game1)gameTab.Game).spriteBatch.Draw(nextFigureTexture, position, Color.White);

            for (int i = 0; i < Constants.FIGUREMAXSIZE; i++)
                for (int j = 0; j < Constants.FIGUREMAXSIZE; j++)
                {
                    Texture2D activeBlock;
                    Color color;
                    if ((i < figure.GetLength(0)) && (j < figure.GetLength(1)))
                    {
                        activeBlock = figure[i, j].blockTexture;
                        color = figure[i, j].color;
                    }
                    else
                    {
                        activeBlock = new Block().blockTexture;
                        color = Block.blankColor;
                    }

                    ((Game1)gameTab.Game).spriteBatch.Draw(activeBlock,
                         position + new Vector2(i * Constants.BLOCKPIXELSIZE + 1, j * Constants.BLOCKPIXELSIZE + 1),
                         null,
                         color,
                         0, Vector2.Zero,
                         1,
                         SpriteEffects.None, 1);
                }
        }

        public void setNewFigure()
        {
            figure = Figures.getFigure();
        }
    }
}
