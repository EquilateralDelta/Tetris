using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class ButtonManager
    {
        SpriteFont font;
        List<Button> buttonList;
        Game game;
        IButtonParent parent;
        int selectedIndex;
        int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if ((buttonList == null) || (buttonList.Count == 0))
                    return;
                buttonList[selectedIndex].isSelect = false;
                if (value > (buttonList.Count - 1))
                    selectedIndex = buttonList.Count - 1;
                else if (value < 0)
                    selectedIndex = 0;
                else
                    selectedIndex = value;
                buttonList[selectedIndex].isSelect = true;
            }
        }

        public ButtonManager(Game game, IButtonParent parent)
        {
            this.game = game;
            this.parent = parent;
            buttonList = new List<Button>();
            SelectedIndex = 0;
        }

        public void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void addButton(Vector2 pos, String text)
        {
            Button newButton = new Button();
            newButton.position = pos;
            newButton.text = text;
            if (buttonList.Count == 0)
                newButton.isSelect = true;
            buttonList.Add(newButton);
        }

        public void addButton(Vector2 pos, String text, List<String> variants)
        {
            addButton(pos, text);
            buttonList.Last().variants = variants;
            buttonList.Last().SelectedVariant = 0;
        }

        public void Draw()
        {
            foreach (var button in buttonList)
                button.Draw((Game1)game, font);
        }

        public void Update()
        {
            if (buttonList.Count == 0)
                return;

            KeyboardState prevState = ((Game1)game).prevKeyState,
                state = ((Game1)game).keyState;

            if (state.IsKeyUp(Keys.Up) && prevState.IsKeyDown(Keys.Up))
                SelectedIndex--;

            if (state.IsKeyUp(Keys.Down) && prevState.IsKeyDown(Keys.Down))
                SelectedIndex++;

            if (state.IsKeyUp(Keys.Enter) && prevState.IsKeyDown(Keys.Enter))
                if (buttonList[SelectedIndex].variants == null)
                    parent.buttonPushed(SelectedIndex);
                else
                    buttonList[SelectedIndex].SelectedVariant++;
        }

        public int getButtonValue(int index)
        {
            return (int)buttonList[index].SelectedVariant;
        }
    }
}
