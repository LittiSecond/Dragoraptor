using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelLoader
    {

        private Campaign _campaign;
        
        private LevelDescriptor _loadedLevel;

        private bool _haveCampaign;
        private bool _haveLevel;


        public void SetCampaign(Campaign campaign)
        {
            _campaign = campaign;
            _haveCampaign = _campaign != null;
            UnloadLevel();
        }

        public LevelDescriptor GetLevelDescriptor(int levelNumber)
        {
            LevelDescriptor newLevel = null;

            if (_haveCampaign)
            {
                if (_loadedLevel != null)
                {
                    if (levelNumber == _loadedLevel.LevelNumber)
                    {
                        newLevel = _loadedLevel;
                    }
                }

                if (newLevel == null)
                {
                    string path = CreateFullPath(levelNumber);
                    newLevel = Resources.Load<LevelDescriptor>(path);

                    if (newLevel)
                    {
                        UnloadLevel();
                        _loadedLevel = newLevel;
                        _haveLevel = true;
                    }
                }
            }
            return newLevel;
        }

        private void UnloadLevel()
        {
            if (_haveLevel)
            {
                Resources.UnloadAsset(_loadedLevel);
                _loadedLevel = null;
                _haveLevel = false;
            }
        }

        private string CreateFullPath(int levelNumber)
        {
            string path = null;

            levelNumber -= 1;
            if (levelNumber < _campaign.LevelPaths.Length)
            {
                path = _campaign.LevelPaths[levelNumber];
            }

            return path;
        }

    }
}