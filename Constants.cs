using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    static class Constants
    {
        static Point windowsize = new Point(530, 600);
        static public Point WINDOWSIZE { get { return windowsize; } }

        static Vector2 fieldblocksize = new Vector2(12, 22);
        static public Vector2 FIELDBLOCKZISE { get { return fieldblocksize; } }

        static int figuremaxsize = 5;
        static public int FIGUREMAXSIZE { get { return figuremaxsize; } }
        
        static int blockpixelsize = 25;
        static public int BLOCKPIXELSIZE { get { return blockpixelsize; } }
        
        static Vector2 nextfigureposition = new Vector2(370, 20);
        static public Vector2 NEXTBLOCKPOSITION { get { return nextfigureposition; } }

        static Vector2 informationposition = new Vector2(370, 200);
        static public Vector2 INFORMATIONPOSITION { get { return informationposition; } }

        static Vector2 gamefieldposition = new Vector2(20, 20);
        static public Vector2 GAMEFIELDPOSITION { get { return gamefieldposition; } }

        static int millisecspermove = 80;
        static public int MILLISECSPERMOVE { get { return millisecspermove; } }

        static int scoreperline = 100;
        static public int SCOREPERLINE { get { return scoreperline; } }

        static int scoremultilinemodifier = 3;
        static public int SCOREMULTILINEMODIFIER { get { return scoremultilinemodifier; } }
    }
}
