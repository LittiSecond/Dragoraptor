using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelProgressInfo
    {
        #region Fields

        private int _levelNumber;

        #endregion


        #region Properties

        public int LevelNumber { get => _levelNumber; }
        public int BestScore { get; set; }
        public LevelStatus Status { get; set; }

        #endregion


        #region ClassLifeCycles

        public LevelProgressInfo(int levelNumber)
        {
            _levelNumber = levelNumber;
        }

        #endregion
    }
}