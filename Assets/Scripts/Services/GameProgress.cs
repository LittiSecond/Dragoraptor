using UnityEngine;

namespace Dragoraptor
{
    public sealed class GameProgress
    {
        
        private const int FIRST_LEVEL_NUMBER = 1;

        private LevelLoader _levelLoader;
        private ProgressData _progressData;
        private LevelDescriptor _currentLevel;
        private ILevelMapView _levelMapView;

        private bool _isCampaignLoaded;


        public GameProgress()
        {
            _progressData = new ProgressData();
        }


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
            _isCampaignLoaded = true;
            
            UpdateUiLevelMap();
        }

        private void PrepareProgressData(Campaign campaign)
        {
            for (int i = 0; i < campaign.LevelPaths.Length; i++)
            {
                _progressData.Levels.Add(new LevelProgressInfo(i + 1));
                _progressData.Levels[i].Status = LevelStatus.NotAvailable;
            }

            _progressData.Levels[0].Status = LevelStatus.Available;
            //_progressData.Levels[1].Status = LevelStatus.NotAvailable;
        }

        public ProgressData GetProgressData()
        {
            return _progressData;
        }

        private void SelectLevel(int levelNumber)
        {
            if (levelNumber >= FIRST_LEVEL_NUMBER && levelNumber <= _progressData.Levels.Count)
            {
                var levelInfo = _progressData.Levels[levelNumber - 1];
                if (levelInfo.Status == LevelStatus.Available || levelInfo.Status == LevelStatus.Finished)
                {
                    if (_progressData.CurrentLevelNumber != levelNumber)
                    {
                        _progressData.CurrentLevelNumber = levelNumber;
                        _currentLevel = _levelLoader.GetLevelDescriptor(levelNumber);
                        _levelMapView.SetLevelSelected(levelNumber);
                    }
                    else
                    {
                        _levelMapView.SetLevelSelected(0);
                        _progressData.CurrentLevelNumber = 0;
                    }
                }
            }
        }

        public void SetLevelMap(ILevelMapView map)
        {
            if (_levelMapView == null)
            {
                _levelMapView = map;
                _levelMapView.OnLevelClick += SelectLevel;
            }
            else
            {
                Debug.LogError("GameProgress->SetLevelMap: duplicated ILevelMapView. ");
            }

            UpdateUiLevelMap();
        }

        private void UpdateUiLevelMap()
        {
            if (_levelMapView == null || !_isCampaignLoaded) return;

            var levels = _progressData.Levels;
            foreach (var level in levels)
            {
                _levelMapView.SetLevelStatus(level.LevelNumber, level.Status);
            }
        }

        public void RegistrateHuntResults(IHuntResults results)
        {
            _progressData.HuntsTotal++;
            _progressData.TotalScore += results.TotalScore;
            _progressData.LastScore = results.TotalScore;
            
            LevelProgressInfo currentLevel = _progressData.Levels[_progressData.CurrentLevelNumber - 1];
            if (currentLevel.BestScore < results.TotalScore)
            {
                currentLevel.BestScore = results.TotalScore;
            }
            
            if (results.IsSucces)
            {
                if (_progressData.CurrentLevelNumber <= _progressData.Levels.Count)
                {
                    int nextLevelNumber = _progressData.CurrentLevelNumber + 1;
                    LevelProgressInfo nextLevel = _progressData.Levels[nextLevelNumber - 1];
                    if (nextLevel.Status == LevelStatus.Hidden || nextLevel.Status == LevelStatus.NotAvailable)
                    {
                        nextLevel.Status = LevelStatus.Available;
                        _levelMapView.SetLevelStatus(nextLevelNumber, LevelStatus.Available);
                    }
                }

                if (currentLevel.Status == LevelStatus.Available)
                {
                    currentLevel.Status = LevelStatus.Finished;
                    _levelMapView.SetLevelStatus(_progressData.CurrentLevelNumber, LevelStatus.Finished);
                    _progressData.CompletedLevels++;
                }
            }
        }

    }
}
