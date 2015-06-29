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

    public class EndComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        static string defealtName = "Gamer";
        Texture2D endImage;
        SpriteFont font;
        HighScore result;

        InputField inputField;

        public EndComponent(Game game, HighScore result)
            : base(game)
        {
            this.result = result;

            inputField = new InputField(Game, new Vector2(100, 280), new Vector2(350, 50), defealtName);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("SpriteFont1");
            endImage = Game.Content.Load<Texture2D>("gameover");

            inputField.LoadContent();

            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            if ((((Game1)Game).keyState.IsKeyUp(Keys.Enter) && ((Game1)Game).prevKeyState.IsKeyDown(Keys.Enter)))
            {
                defealtName = inputField.text;
                result.name = inputField.text;
                HighScoresComponent.addBest(result);
                ((Game1)Game).ChangeState(Game1.GameStates.Menu);
            }
            inputField.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((Game1)Game).spriteBatch.Draw(endImage,
                new Vector2((Game.GraphicsDevice.Viewport.Width - endImage.Width) / 2, 30),
                Color.White);

            ((Game1)Game).spriteBatch.DrawString(font, "Input your name:",
                new Vector2((Game.GraphicsDevice.Viewport.Width - font.MeasureString("Input your name:").X) / 2, 220),
                Color.LawnGreen);

            ((Game1)Game).spriteBatch.DrawString(font, "Score: " + result.score.ToString(),
                new Vector2((Game.GraphicsDevice.Viewport.Width - font.MeasureString("Score: " + result.score.ToString()).X) / 2, 350),
                Color.LawnGreen);

            ((Game1)Game).spriteBatch.DrawString(font, "Level: " + result.level.ToString(),
                new Vector2((Game.GraphicsDevice.Viewport.Width - font.MeasureString("Level: " + result.level.ToString()).X) / 2, 400),
                Color.LawnGreen);

            ((Game1)Game).spriteBatch.DrawString(font, "Difficulty: " + result.difficult.ToString(),
                new Vector2((Game.GraphicsDevice.Viewport.Width - font.MeasureString("Difficulty: " + result.difficult.ToString()).X) / 2, 450),
                Color.LawnGreen);

            inputField.Draw();

            base.Draw(gameTime);
        }
    }
}
