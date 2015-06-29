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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameTab : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public GameField gameField { get; set; }
        public NextFigure nextFigure { get; set; }
        public Information information { get; set; }
        public DifficultyVariant difficult;

        public GameTab(Game game, int difficultIndex)
            : base(game)
        {
            information = new Information(this);
            nextFigure = new NextFigure(this);
            gameField = new GameField(this);
            difficult = Difficulty.getDifficult(difficultIndex);
            information.difVariant = (Difficulty.Variants)difficultIndex;
        }

        protected override void LoadContent()
        {
            information.LoadContent();
            nextFigure.LoadContent();
            gameField.LoadContent();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            information.Update(gameTime);
            gameField.Update(gameTime);



            if (((Game1)Game).keyState.IsKeyUp(Keys.Escape) && ((Game1)Game).prevKeyState.IsKeyDown(Keys.Escape))
                ((Game1)Game).ChangeState(Game1.GameStates.Pause);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameField.Draw();
            nextFigure.Draw();
            information.Draw();

            base.Draw(gameTime);
        }
    }
}
