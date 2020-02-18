using System;

namespace Assets.Scripts.Game
{
    public class ScoreTracker
    {
        private int score;


        public event Action<int> ScoreChanged = delegate { };
    public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                ScoreChanged(score);
            }
        }

      
        public void IncrementScore()
        {
            Score++;
            
        }
    }
}

