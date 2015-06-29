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

    public class Game1 : Microsoft.Xna.Framework.Game
    {


        static Random rand = new Random();
        static public Random Rand { get { return rand; } }

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch { get; private set; }
        GameTab gameTab;
        HighScoresComponent highScoresComponent;
        MenuComponent menuComponent;
        PauseComponent pauseComponent;
        EndComponent endComponent;
        GameStates gameState;

        public KeyboardState keyState { get; private set; }
        public KeyboardState prevKeyState { get; private set; }

        Texture2D background;
        public enum GameStates { Start, Menu, HighScores ,Game, Pause, End }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Constants.WINDOWSIZE.X;
            graphics.PreferredBackBufferHeight = Constants.WINDOWSIZE.Y;
            
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            menuComponent = new MenuComponent(this);
            Components.Add(menuComponent);
            gameState = GameStates.Menu;

            keyState = Keyboard.GetState();
            prevKeyState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Content.Load<Texture2D>("bg");
            Block.patternBlockTexture = Content.Load<Texture2D>("green shape");

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void ChangeState(GameStates newState)
        {
            switch (newState)
            {
                case GameStates.Menu:
                    if (gameState == GameStates.Start)
                    {
                        menuComponent = new MenuComponent(this);
                        Components.Add(menuComponent);
                    }
                    else if (gameState == GameStates.HighScores)
                    {
                        Components.Remove(highScoresComponent);

                        menuComponent.Enabled = true;
                        menuComponent.Visible = true;
                    }
                    else
                    {
                        Components.Remove(gameTab);
                        Components.Remove(pauseComponent);
                        if (gameState == GameStates.End)
                            Components.Remove(endComponent);

                        menuComponent.Enabled = true;
                        menuComponent.Visible = true;
                    }
                    break;
                
                case GameStates.HighScores:
                    menuComponent.Enabled = false;
                    menuComponent.Visible = false;
                    highScoresComponent = new HighScoresComponent(this);
                    Components.Add(highScoresComponent);
                    break;

                case GameStates.Game:
                    if (gameState == GameStates.Menu)
                    {
                        menuComponent.Enabled = false;
                        menuComponent.Visible = false;

                        gameTab = new GameTab(this, menuComponent.buttonManager.getButtonValue(0));
                        Components.Add(gameTab);

                        pauseComponent = new PauseComponent(this);
                        pauseComponent.Enabled = false;
                        pauseComponent.Visible = false;
                        Components.Add(pauseComponent);
                    }
                    else if (gameState == GameStates.Pause)
                    {
                        gameTab.Enabled = true;
                        pauseComponent.Enabled = false;
                        pauseComponent.Visible = false;
                    }
                    break;
                    

                case GameStates.Pause:
                    gameTab.Enabled = false;
                    pauseComponent.Enabled = true;
                    pauseComponent.Visible = true;
                    break;

                case GameStates.End:
                    gameTab.Enabled = false;
                    gameTab.Visible = false;
                    pauseComponent.Enabled = false;
                    pauseComponent.Visible = false;
                    endComponent = new EndComponent(this, gameTab.information.FormScore());
                    Components.Add(endComponent);
                    break;
            }

            gameState = newState;
        }
    }
}
