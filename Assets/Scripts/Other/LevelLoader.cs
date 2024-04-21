using UnityEngine;


namespace Dragoraptor
{
    public sealed class LevelLoader
    {

        private Campaign _campaign;
        
        private LevelDescriptor _loadedLevel;
        private int _loadedLevelNumber;

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
            if (levelNumber <= 0 || levelNumber > _campaign.LevelPaths.Length) return null;
            if (!_haveCampaign) return null;
            
            LevelDescriptor newLevel = null;

            if (_loadedLevelNumber > 0)
            {
                if (levelNumber == _loadedLevelNumber)
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
                    _loadedLevelNumber = levelNumber;
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
                _loadedLevelNumber = -1;
            }
        }

        private string CreateFullPath(int levelNumber)
        {
            //string path = null;

            // int index = levelNumber - 1;
            // if (index < _campaign.LevelPaths.Length)
            // {
            //     path = _campaign.LevelPaths[index];
            // }

            return _campaign.LevelPaths[levelNumber - 1];
        }

    }
}