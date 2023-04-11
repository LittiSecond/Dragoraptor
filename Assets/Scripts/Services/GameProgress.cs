namespace Dragoraptor
{
    public sealed class GameProgress
    {
        #region Fields

        private const int FIRST_LEVEL_NUMBER = 1;

        private LevelLoader _levelLoader;
        private ProgressData _progressData;
        private LevelDescriptor _currentLevel;

        #endregion


        #region ClassLifeCycles

        public GameProgress()
        {
            _progressData = new ProgressData();
        }

        #endregion


        #region Methods

        public void ChooseNextLevel()
        {
            if (_progressData.CurrentLevelNumber == 0)
            {
                _progressData.CurrentLevelNumber = FIRST_LEVEL_NUMBER;

            }
            else
            {
                _progressData.CurrentLevelNumber++;
                if (_progressData.CurrentLevelNumber > _progressData.Levels.Count)
                {
                    _progressData.CurrentLevelNumber = FIRST_LEVEL_NUMBER;
                }
            }

            _currentLevel = _levelLoader.GetLevelDescriptor(_progressData.CurrentLevelNumber);
        }

        public LevelDescriptor GetCurrentLevel()
        {
            return _currentLevel;
        }

        public void SetCampaign(Campaign campaign)
        {
            if (_levelLoader == null)
            {
                _levelLoader = new LevelLoader();
            }
            _levelLoader.SetCampaign(campaign);
            _progressData.Clear();
            PrepareProgressData(campaign);
        }

        private void PrepareProgressData(Campaign campaign)
        {
            for (int i = 0; i < campaign.LevelPaths.Length; i++)
            {
                _progressData.Levels.Add(new LevelProgressInfo(i + 1));
            }

            _progressData.Levels[0].Status = LevelStatus.Avilable;
            _progressData.Levels[1].Status = LevelStatus.Avilable;
        }

        public ProgressData GetProgressData()
        {
            return _progressData;
        }

        #endregion

    }
}
