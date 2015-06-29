using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public struct DifficultyVariant
    {
        public int millisecsPerTick;
        public int millisecsPerLevel;
        public int maxLevel;
        public float levelModifier;
    }

    public class Difficulty
    {
        public enum Variants { children, easy, medium, hard, flash };
        static List<DifficultyVariant> difficultList = constructDifficulties();

        static List<DifficultyVariant> constructDifficulties()
        {
            var result = new List<DifficultyVariant>();
            DifficultyVariant newVariant;

            newVariant = getVariant(800, 60000, 10, .2f);
            result.Add(newVariant);

            newVariant = getVariant(500, 30000, 10, .2f);
            result.Add(newVariant);

            newVariant = getVariant(350, 20000, 10, .2f);
            result.Add(newVariant);

            newVariant = getVariant(220, 10000, 10, .2f);
            result.Add(newVariant);

            newVariant = getVariant(100, 10000, 10, .15f);
            result.Add(newVariant);
            
            return result;
        }

        static DifficultyVariant getVariant(int pTick, int pLevel, int mLevel, float lMod)
        {
            var result = new DifficultyVariant();
            result.millisecsPerTick = pTick;
            result.millisecsPerLevel = pLevel;
            result.maxLevel = mLevel;
            result.levelModifier = lMod;
            return result;
        }

        public static DifficultyVariant getDifficult(int index)
        {
            return difficultList[index];
        }
    }
}
