namespace Dragoraptor
{
    public sealed class GameProgress
    {
        #region Fields

        private const int FIRST_LEVEL_NUMBER = 1;
        private const int LAST_LEVEL_NUMBER = 2;

        private LevelDescriptor _currentLevel;

        #endregion


        #region Methods

        public void ChooseNextLevel()
        {
            if (_currentLevel == null)
            {
                _currentLevel = PrefabLoader.GetLevelDescriptor(FIRST_LEVEL_NUMBER);
            }
            else
            {
                int levelNumber = _currentLevel.LevelNumber + 1;
                if (levelNumber > LAST_LEVEL_NUMBER)
                {
                    levelNumber = FIRST_LEVEL_NUMBER;
                }
                _currentLevel = PrefabLoader.GetLevelDescriptor(levelNumber);
            }
        }

        public LevelDescriptor GetCurrentLevel()
        {
            return _currentLevel;
        }

        #endregion

    }
}
