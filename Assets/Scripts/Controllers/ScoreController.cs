using System;


namespace Dragoraptor
{
    public sealed class ScoreController : IScoreSource
    {

        private int _score;


        public void ClearScore()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void AddScore(int amount)
        {
            if (amount > 0)
            {
                _score += amount;
                OnScoreChanged?.Invoke(_score);
            }
        }


        #region IScoreSource

        public event Action<int> OnScoreChanged;

        public int GetScore()
        {
            return _score;
        }

        #endregion
    }
}