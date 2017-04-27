using System;

namespace GameObjects
{
    public class HighScore : IComparable
    {
        public string name;
        public int score;

        public int CompareTo(object obj)
        {
            var otherScore = ((HighScore)obj).score;

            if (score > otherScore)
            {
                return 1;
            }
            else if (score < otherScore)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
