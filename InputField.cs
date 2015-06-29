using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class InputField
    {
        SpriteFont font;
        Game game;
        Vector2 position, size;
        Texture2D pixel;
        KeyboardState lastState;

        public bool isActive { get; set; }
        public string text { get; set; }

        public InputField(Game game, Vector2 position, Vector2 size, string text = "")
        {
            this.game = game;
            this.position = position;
            this.size = size;
            this.text = text;
            isActive = true;
            lastState = Keyboard.GetState();
        }

        public void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("SpriteFont1");
            pixel = game.Content.Load<Texture2D>("pixel");
        }

        public void Update()
        {
            if (isActive)
            {
                KeyboardState state = Keyboard.GetState();
                foreach (var key in lastState.GetPressedKeys())
                    if (state.IsKeyUp(key))
                    {
                        if ((key >= Keys.D0) && (key <= Keys.D9))
                        {
                            text += key.ToString()[1];
                        }
                        else if (key == Keys.Space)
                            text += " ";
                        else if ((key >= Keys.A) && (key <= Keys.Z))
                        {
                            if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
                                text += key.ToString().ToUpper();
                            else
                                text += key.ToString().ToLower();
                        }
                        else if (key == Keys.Back)
                        {
                            if (text.Length > 0)
                                text = text.Remove(text.Length - 1, 1);
                        }

                        if (text.Length > 15)
                            text = text.Remove(0, text.Length - 15);
                    }

                lastState = state;
            }
        }

        public void Draw()
        {
            ((Game1)game).spriteBatch.Draw(pixel, position, null,
                Color.Black, 0, Vector2.Zero, size, SpriteEffects.None, 1);
            ((Game1)game).spriteBatch.Draw(pixel, position + new Vector2(1, 1), null,
                Color.White, 0, Vector2.Zero, size - new Vector2(2, 2), SpriteEffects.None, 1);
            ((Game1)game).spriteBatch.DrawString(font, text, position + size / 2 - font.MeasureString(text) / 2, Color.LawnGreen);
        }
    }
}
