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
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent, IButtonParent
    {
        SpriteFont font;
        Texture2D titleImage;
        public ButtonManager buttonManager;

        public MenuComponent(Game game)
            : base(game)
        {
            buttonManager = new ButtonManager(game, this);
        }

        public override void Initialize()
        {
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 300), "Difficulty", 
                new List<string> { "children's", "easy", "medium", "hard", "flash" });
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 370), "Start");
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 440), "HighScores");
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 510), "Quit");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            buttonManager.Update();

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("SpriteFont1");
            titleImage = Game.Content.Load<Texture2D>("title");
            buttonManager.LoadContent();

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ((Game1)Game).spriteBatch.Draw(titleImage,
                new Vector2((Game.GraphicsDevice.Viewport.Width - titleImage.Width) / 2, 30),
                Color.White);

            buttonManager.Draw();

            base.Draw(gameTime);
        }

        void IButtonParent.buttonPushed(int index)
        {
            switch (index)
            {
                case 1:
                    ((Game1)Game).ChangeState(Game1.GameStates.Game);
                    break;

                case 2:
                    ((Game1)Game).ChangeState(Game1.GameStates.HighScores);
                    break;

                case 3:
                    Game.Exit();
                    break;
            }
        }
    }
}
