using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ColorConversion;

namespace Tetris
{
    public class Block
    {
        public static Texture2D patternBlockTexture { get; set; }
        public bool isFill { get; set; }
        Texture2D _blockTexture;
        public Texture2D blockTexture {
            get
            {
                if (isFill)
                    return _blockTexture;
                else
                    return patternBlockTexture;
            }
            private set { _blockTexture = value; }
        }
        public Color color {
            get
            {
                if (isFill)
                    return Color.White;
                else
                    return blankColor;
            }
        }
        public int hue { get; private set; }

        static public Color blankColor = Color.Transparent;

        public Block(int hue)
        {
            _blockTexture = new Texture2D(patternBlockTexture.GraphicsDevice, patternBlockTexture.Width, patternBlockTexture.Height);
            Color[] pixels = new Color[patternBlockTexture.Width * patternBlockTexture.Height];
            patternBlockTexture.GetData<Color>(pixels);
            for (int i = 0; i < pixels.Length; i++)
            {
                ColorHSL HSLpixel = ColorConversion.ColorConversion.RGBtoHSL(pixels[i]);
                HSLpixel.H = hue;
                pixels[i] = ColorConversion.ColorConversion.HSLtoRGB(HSLpixel);
            }
            _blockTexture.SetData<Color>(pixels);

            this.hue = hue;
            this.isFill = true;
        }

        public Block()
        {
            this.isFill = false;
        }

        ~Block()
        {
            if (isFill)
                _blockTexture.Dispose();
        }

        static public Block[,] getBlinkArray(Vector2 size)
        {
            if ((size.X < 1) || (size.Y < 1))
                return null;

            Block[,] result = new Block[(int)size.X, (int)size.Y];
            for (int i = 0; i < size.X; i++)
                for (int j = 0; j < size.Y; j++)
                    result[i, j] = new Block();

            return result;
        }
    }
}
