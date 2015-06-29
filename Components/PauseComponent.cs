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
    public class PauseComponent : Microsoft.Xna.Framework.DrawableGameComponent, IButtonParent
    {
        SpriteFont font;
        Texture2D pauseImage;
        Texture2D shadePixel;
        int timerPreventExit;
        public ButtonManager buttonManager;

        public PauseComponent(Game game)
            : base(game)
        {
            buttonManager = new ButtonManager(Game, this);
            timerPreventExit = 0;
        }

        protected override void LoadContent()
        {
            pauseImage = Game.Content.Load<Texture2D>("pause");
            font = Game.Content.Load<SpriteFont>("SpriteFont1");
            buttonManager.LoadContent();

            shadePixel = new Texture2D(pauseImage.GraphicsDevice, 1, 1);
            Color[] pixel = new Color[1];
            pixel[0] = Color.DarkGray;
            pixel[0].A = 150;
            shadePixel.SetData<Color>(pixel);

            base.LoadContent();
        }

        public override void Initialize()
        {
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 400), "Resume");
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 450), "End");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).keyState.IsKeyUp(Keys.Escape) && ((Game1)Game).prevKeyState.IsKeyDown(Keys.Escape) && (timerPreventExit != 0))
                ((Game1)Game).ChangeState(Game1.GameStates.Game);

            buttonManager.Update();

            timerPreventExit++;

            base.Update(gameTime);
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            timerPreventExit = 0;

            base.OnEnabledChanged(sender, args);
        } 

        public override void Draw(GameTime gameTime)
        {
            ((Game1)Game).spriteBatch.Draw(shadePixel, new Vector2(0, 0),
                null, Color.White, 0, Vector2.Zero,
                new Vector2(Constants.WINDOWSIZE.X, Constants.WINDOWSIZE.Y),
                SpriteEffects.None, 1);

            ((Game1)Game).spriteBatch.Draw(pauseImage,
                new Vector2((Game.GraphicsDevice.Viewport.Width - pauseImage.Width) / 2,
                    (Game.GraphicsDevice.Viewport.Height - pauseImage.Height) / 2),
                Color.White);

            buttonManager.Draw();
            
            base.Draw(gameTime);
        }

        public void buttonPushed(int index)
        {
            switch (index)
            {
                case 0:
                    ((Game1)Game).ChangeState(Game1.GameStates.Game);
                    break;
                case 1:
                    
                    ((Game1)Game).ChangeState(Game1.GameStates.End);
                    break;
            }
        }
    }
}
