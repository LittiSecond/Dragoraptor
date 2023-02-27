using UnityEngine;


namespace Dragoraptor
{
    public sealed class SceneController
    {
        #region Fields

        private GameObject _ground;
        private GameObject _backGround;
        private LevelDescriptor _currentLevel;

        private int _currentLevelNumber;

        private bool _isMainScreen;
        private bool _isLevelActive;
        private bool _isLevelCreated;

        #endregion


        #region Methods

        public void SetMainScreenScene()
        {
            if (_isLevelActive)
            {
                DeactivateLevel();
            }
            _isMainScreen = true;
        }

        public void LoadLevel(int levelNumber)
        {
            if (levelNumber > 0)
            {
                if (_isMainScreen)
                {
                    DeactivateMainScreen();
                }

                if (_currentLevelNumber != levelNumber)
                {
                    DeactivateLevel();
                    DestroyLevel();

                    _currentLevel = PrefabLoader.GetLevelDescriptor(levelNumber);
                    _currentLevelNumber = _currentLevel.LevelNumber;
                    CreateLevel();
                }
                else
                {
                    if (!_isLevelActive)
                    {
                        ActivateLevel();
                    }
                }
            }
        }

        private void ActivateLevel()
        {
            if (!_isLevelActive)
            {
                _ground.SetActive(true);
                _backGround.SetActive(true);
                _isLevelActive = true;
            }
        }

        private void DeactivateLevel()
        {
            if (_isLevelActive)
            {
                _ground.SetActive(false);
                _backGround.SetActive(false);
                _isLevelActive = false;
            }
        }

        private void DestroyLevel()
        {
            if (_isLevelCreated)
            {
                GameObject.Destroy(_backGround);
                _backGround = null;
                GameObject.Destroy(_ground);
                _ground = null;
                _isLevelCreated = false;
            }
        }

        private void DeactivateMainScreen()
        {
            _isMainScreen = false;
        }

        private void CreateLevel()
        {
            GameObject prefab = _currentLevel.BackgroundPrefab;
            _backGround = GameObject.Instantiate(prefab);
            prefab = _currentLevel.GroundPrefab;
            _ground = GameObject.Instantiate(prefab);
            _isLevelCreated = true;
            _isLevelActive = true;
        }

        #endregion

    }
}
