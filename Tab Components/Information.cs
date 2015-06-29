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
    public class Information
    {
        GameTab gameTab;

        SpriteFont font;

        public int score { get; private set; }
        public int level { get; private set; }
        public Difficulty.Variants difVariant { get; set; }
        int levelTimer;

        public HighScore FormScore()
        {
            return new HighScore("", score,
                difVariant, level);
        }

        public Information(GameTab gameTab)
        {
            this.gameTab = gameTab;

            score = 0;
            level = 1;
            levelTimer = 0;
        }

        public void LoadContent()
        {
            font = gameTab.Game.Content.Load<SpriteFont>("SpriteFont");
        }

        public void Draw()
        {
            ((Game1)gameTab.Game).spriteBatch.DrawString(font, "Score: ", Constants.INFORMATIONPOSITION, Color.Wheat);
            ((Game1)gameTab.Game).spriteBatch.DrawString(font, score.ToString(), Constants.INFORMATIONPOSITION + new Vector2(0, 30), Color.Wheat);
            ((Game1)gameTab.Game).spriteBatch.DrawString(font, "Level: ", Constants.INFORMATIONPOSITION + new Vector2(0, 60), Color.Wheat);
            ((Game1)gameTab.Game).spriteBatch.DrawString(font, level.ToString(), Constants.INFORMATIONPOSITION + new Vector2(0, 90), Color.Wheat);
            ((Game1)gameTab.Game).spriteBatch.DrawString(font, "Esc to Pause", Constants.INFORMATIONPOSITION + new Vector2(0, 150), Color.Wheat);
        }

        public void Update(GameTime gameTime)
        {
            levelTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (levelTimer > gameTab.difficult.millisecsPerLevel)
            {
                if (level < gameTab.difficult.maxLevel)
                    level++;

                levelTimer -= gameTab.difficult.millisecsPerLevel;
            }
        }

        public void addScore(int lines)
        {
            if (lines == 0)
                return;

            score += Constants.SCOREPERLINE *
                (int)Math.Pow(Constants.SCOREMULTILINEMODIFIER, lines - 1) *
                (int)Math.Floor((float)level / 10 + 1);
        }
    }
}
