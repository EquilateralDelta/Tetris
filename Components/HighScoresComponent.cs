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
    public class HighScoresComponent : Microsoft.Xna.Framework.DrawableGameComponent, IButtonParent
    {
        static List<HighScore> scores = HighScore.Load();
        public ButtonManager buttonManager;
        List<HighScore> activeScore;
        int currentDifficult;

        SpriteFont font;

        public HighScoresComponent(Game game)
            : base(game)
        {
            buttonManager = new ButtonManager(Game, this);
            activeScore = new List<HighScore>();
        }

        static public void addBest(HighScore newScore)
        {
            scores.Add(newScore);
            HighScore.Save(scores);
        }

        void formList()
        {
            currentDifficult = buttonManager.getButtonValue(1);
            activeScore.Clear();
            foreach (var score in scores)
                if ((int)score.difficult == currentDifficult)
                {
                    bool isAdded = false;

                    for (int i = 0; i < activeScore.Count; ++i)
                        if (activeScore[i].score < score.score)
                        {
                            activeScore.Insert(i, score);
                            isAdded = true;
                            break;
                        }

                    if (!isAdded)
                        activeScore.Add(score);
                }

            while (activeScore.Count > 7)
            {
                scores.Remove(activeScore[7]);
                activeScore.RemoveRange(7, 1);
            }
        }

        public override void Initialize()
        {
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 60), "Back");
            buttonManager.addButton(new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 120), "Difficly",
                new List<string> { "children's", "easy", "medium", "hard", "flash" });

            formList();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            buttonManager.Update();
            
            if (currentDifficult != buttonManager.getButtonValue(1))
                formList();

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            buttonManager.LoadContent();
            font = Game.Content.Load<SpriteFont>("SpriteFont1");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            buttonManager.Draw();

            int pos = 0;
            foreach (var score in activeScore)
                ((Game1)Game).spriteBatch.DrawString(font, score.name + " : " + score.score, new Vector2(100, 200 + (pos++) * 50), Color.LightGreen);

            base.Draw(gameTime);
        }

        void IButtonParent.buttonPushed(int index)
        {
            switch (index)
            {
                case 0:
                    ((Game1)Game).ChangeState(Game1.GameStates.Menu);
                    break;
            }
        }
    }
}
