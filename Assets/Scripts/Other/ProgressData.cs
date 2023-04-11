using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class ProgressData
    {
        #region Fields

        public List<LevelProgressInfo> Levels;
        public int ComplitedLevels;
        public int HuntsTotal;
        public int TotalScore;
        public int LastScore;
        public int CurrentLevelNumber;

        #endregion


        #region ClassLifeCycles

        public ProgressData()
        {
            Levels = new List<LevelProgressInfo>();
        }

        #endregion


        #region Methods

        public void Clear()
        {
            Levels.Clear();
            ComplitedLevels = 0;
            HuntsTotal = 0;
            TotalScore = 0;
            LastScore = 0;
            CurrentLevelNumber = 0;
        }

        #endregion
    }
}