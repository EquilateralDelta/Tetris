using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public interface IButtonParent
    {
        void buttonPushed(int index);
    }

    class Button
    {
        public Vector2 position;
        public bool isSelect;
        public String text;
        public List<String> variants;
        int selectedVariant;
        public int? SelectedVariant
        {
            get
            {
                if (variants == null)
                    return null;
                return selectedVariant;
            }
            set
            {
                selectedVariant = (int)value % variants.Count;        
            }
        }

        public void Draw(Game1 game, SpriteFont font)
        {
            Color color;
            if (!isSelect)
                color = Color.LawnGreen;
            else
                color = Color.Red;
            String drawText = text;
            if (variants != null)
                drawText += ": " + variants[selectedVariant];
            game.spriteBatch.DrawString(font, drawText, position - font.MeasureString(drawText) / 2, color);
        }
    }
}
