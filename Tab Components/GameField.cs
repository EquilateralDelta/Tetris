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
    public class GameField 
    {
        GameTab gameTab;

        Texture2D fieldTexture;

        Block[,] field;
        MovingFigure movingFigure;
        int ticktimer;
        
        int movetimer;
        bool isMoved;

        public GameField(GameTab gameTab)
        {
            this.gameTab = gameTab;

            field = Block.getBlinkArray(Constants.FIELDBLOCKZISE);
            movingFigure = new MovingFigure();
            setNewFigure();

            ticktimer = 0;
            movetimer = 0;
            isMoved = false;
        }

        public void Update(GameTime gameTime)
        {
            int millisecsAdd = gameTime.ElapsedGameTime.Milliseconds;

            KeyboardState state = ((Game1)gameTab.Game).keyState;
            KeyboardState prevState = ((Game1)gameTab.Game).prevKeyState;
            if (state.IsKeyDown(Keys.Down))
                millisecsAdd = (int)(gameTab.difficult.millisecsPerTick / 2.5);
            else
            {
                if (state.IsKeyUp(Keys.Up) && prevState.IsKeyDown(Keys.Up))
                    if (!isCollide(movingFigure.getTurned(), movingFigure.Position))
                        movingFigure.Turn();

                if ((state.IsKeyDown(Keys.Left) || prevState.IsKeyDown(Keys.Left))
                    && (state.IsKeyDown(Keys.Right) || prevState.IsKeyDown(Keys.Right)))
                    movetimer = 0;
                else
                {
                    if (state.IsKeyUp(Keys.Left) && prevState.IsKeyDown(Keys.Left))
                    {
                        if (!isCollide(movingFigure.figure, new Vector2(movingFigure.Position.X - 1, movingFigure.Position.Y))
                            && !isMoved)
                            movingFigure.MoveLeft();

                        movetimer = 0;
                        isMoved = false;
                    }

                    else if (state.IsKeyUp(Keys.Right) && prevState.IsKeyDown(Keys.Right))
                    {
                        if (!isCollide(movingFigure.figure, new Vector2(movingFigure.Position.X + 1, movingFigure.Position.Y))
                            && !isMoved)
                            movingFigure.MoveRight();

                        movetimer = 0;
                        isMoved = false;
                    }

                    else if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Right))
                    {
                        movetimer += millisecsAdd;
                        if (movetimer > Constants.MILLISECSPERMOVE)
                        {
                            isMoved = true;
                            movetimer -= Constants.MILLISECSPERMOVE;
                            if (state.IsKeyDown(Keys.Left) && state.IsKeyUp(Keys.Right))
                            {
                                if (!isCollide(movingFigure.figure, new Vector2(movingFigure.Position.X - 1, movingFigure.Position.Y)))
                                    movingFigure.MoveLeft();
                            }
                            else if (state.IsKeyUp(Keys.Left) && state.IsKeyDown(Keys.Right))
                                if (!isCollide(movingFigure.figure, new Vector2(movingFigure.Position.X + 1, movingFigure.Position.Y)))
                                    movingFigure.MoveRight();
                        }
                    }
                    millisecsAdd = (int)(millisecsAdd * (gameTab.difficult.levelModifier * gameTab.information.level + 1));
                }
            }

            ticktimer += millisecsAdd;
            if (ticktimer > gameTab.difficult.millisecsPerTick)
            {
                ticktimer -= gameTab.difficult.millisecsPerTick;

                if (isCollide(movingFigure.figure, new Vector2(movingFigure.Position.X, movingFigure.Position.Y + 1)))
                {
                    FixFigure(movingFigure.figure, movingFigure.Position);
                    deleteFullLines();
                    setNewFigure();
                }
                else
                    movingFigure.MoveDown();
            }
        }

        public void LoadContent()
        {
            fieldTexture = gameTab.Game.Content.Load<Texture2D>("field");
        }

        public void Draw()
        {
            //((Game1)gameTab.Game).bloomComponent.BeginDraw();

            ((Game1)gameTab.Game).spriteBatch.Draw(fieldTexture, Constants.GAMEFIELDPOSITION, Color.White);

            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    ((Game1)gameTab.Game).spriteBatch.Draw(field[i, j].blockTexture,
                        Constants.GAMEFIELDPOSITION + new Vector2(i * Constants.BLOCKPIXELSIZE + 1, j * Constants.BLOCKPIXELSIZE + 1),
                        null,
                        field[i, j].color,
                        0, Vector2.Zero,
                        1,
                        SpriteEffects.None, 1);

            for (int i = 0; i < movingFigure.figure.GetLength(0); i++)
                for (int j = 0; j < movingFigure.figure.GetLength(1); j++)
                    if (movingFigure.figure[i, j].isFill && ((j + movingFigure.Position.Y) >= 0))
                        ((Game1)gameTab.Game).spriteBatch.Draw(movingFigure.figure[i, j].blockTexture,
                            Constants.GAMEFIELDPOSITION + new Vector2((i + movingFigure.Position.X) * Constants.BLOCKPIXELSIZE + 1, 
                            (j + movingFigure.Position.Y) * Constants.BLOCKPIXELSIZE + 1),
                            null,
                            movingFigure.figure[i, j].color,
                            0, Vector2.Zero,
                            1,
                            SpriteEffects.None, 1);
        }

        private void FixFigure(Block[,] figure, Vector2 position)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
                for (int j = 0; j < figure.GetLength(1); j++)
                    if (figure[i, j].isFill)
                        if ((int)position.Y + j >= 0)
                            field[(int)position.X + i, (int)position.Y + j] = new Block(figure[i, j].hue);
                        else
                        {
                            ((Game1)gameTab.Game).ChangeState(Game1.GameStates.End);
                            return;
                        }
        }

        private bool isCollide(Block[,] figure, Vector2 position)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    Block check;
                    if (((position.X + i) < 0) || ((position.X + i) >= field.GetLength(0)) || ((position.Y + j) >= field.GetLength(1)))
                        check = new Block(0);
                    else if ((position.Y + j) < 0)
                        check = new Block();
                    else
                        check = field[(int)position.X + i, (int)position.Y + j];

                    if (figure[i, j].isFill && check.isFill)
                        return true;
                }

            return false;
        }

        void deleteFullLines()
        {
            int counter = 0;
            for (int i = 0; i < field.GetLength(1); i++)
                if (checkLine(i))
                {
                    deleteLine(i);
                    counter++;
                }

            gameTab.information.addScore(counter);
        }
        void deleteLine(int index)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = index; j > 0; j--)
                    field[i, j] = field[i, j - 1];

                field[i, 0] = new Block();
            }
            
        }
        bool checkLine(int index)
        {
            for (int i = 0; i < field.GetLength(0); i++)
                if (!field[i, index].isFill)
                    return false;
            
            return true;
        }
        void setNewFigure()
        {
            movingFigure.setFigure(gameTab.nextFigure.figure);
            gameTab.nextFigure.setNewFigure();
        }
    }
}
