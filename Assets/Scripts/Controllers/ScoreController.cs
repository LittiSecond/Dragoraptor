using System;


namespace Dragoraptor
{
    public sealed class ScoreController : IScoreSource
    {
        #region Fields

        private int _score;

        #endregion

        #region Methods

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

        #endregion


        #region IScoreSource

        public event Action<int> OnScoreChanged;

        public int GetScore()
        {
            return _score;
        }

        #endregion
    }
}