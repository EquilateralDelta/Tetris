using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Tetris
{
    [Serializable]
    public class HighScore
    {
        public string name;
        public int score;
        public Difficulty.Variants difficult;
        public int level;

        public HighScore(string name, int score, Difficulty.Variants difficult, int level)
        {
            this.name = name;
            this.score = score;
            this.difficult = difficult;
            this.level = level;
        }

        static public List<HighScore> Load()
        {
            List<HighScore> res = new List<HighScore>();
            BinaryFormatter bf = new BinaryFormatter();
            if (!File.Exists("highscores.bat"))
                return res;
            FileStream read = File.Open("highscores.bat", FileMode.Open);
            res = (List<HighScore>)bf.Deserialize(read);
            read.Close();
            return res;
        }

        static public void Save(List<HighScore> scores)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream read = File.Open("highscores.bat", FileMode.Create);
            bf.Serialize(read, scores);
            read.Close();
        }
    }
}
