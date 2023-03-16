using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class GameStateManager
    {
        #region PrivateData

        private enum GameState
        {
            None        = 0,
            MainScreen  = 1,
            Hunt        = 2
        }

        #endregion


        #region Fields

        private UiManager _uiManager;
        private PlayerCharacterController _characterController;
        private SceneController _sceneController;
        private NpcManager _npcManager;
        private IOnceInitializable[] _initAtStartHunt;

        private GameState _state;

        private bool _isPause;
        private bool _isInitializedAtStartHunt;

        #endregion


        #region Methods

        public void SetControllers(PlayerCharacterController pcc, SceneController sc, NpcManager nm, 
            IOnceInitializable[] initAtStartHunt)
        {
            _characterController = pcc;
            _sceneController = sc;
            _npcManager = nm;
            _initAtStartHunt = initAtStartHunt;
        }

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;
                _sceneController.SetMainScreenScene();
            }
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        public void SwitchToMainScreen()
        {
            if (_state == GameState.Hunt)
            {
                _state = GameState.MainScreen;
                SwitchPause(false);

                _npcManager.StopNpcSpawn();
                _npcManager.ClearNpc();
                DeactivateCharacterControll();
                _characterController.RemoveCharacter();
                _sceneController.SetMainScreenScene();
                _uiManager.SwichToMainScreen();
            }
        }

        public void SwitchToHunt()
        {
            if (_state == GameState.MainScreen)
            {
                _state = GameState.Hunt;
                Services.Instance.GameProgress.ChooseNextLevel();

                _uiManager.SwichToHuntScreen();

                if (!_isInitializedAtStartHunt)
                {
                    _isInitializedAtStartHunt = true;
                    for (int i = 0; i < _initAtStartHunt.Length; i++)
                    {
                        _initAtStartHunt[i].OnceInitialize();
                    }
                }

                _sceneController.BuildLevel();
                _characterController.CreateCharacter();
                ActivateCharacterControll();
                _npcManager.PrepareNpcSpawn();
                _npcManager.RestartNpcSpawn();
            }
        }

        public void CharacterKilled()
        {
            Services.Instance.UiFactory.GetHuntScreen().ShowDefeatMenu();
        }

        private void ActivateCharacterControll()
        {
            _characterController.CharacterControllOn();
        }

        private void DeactivateCharacterControll()
        {
            _characterController.CharacterControllOff();
        }

        public void OnMenuOpened()
        {
            if (_state == GameState.Hunt)
            {
                SwitchPause(true);
            }
        }

        public void OnMenuClosed()
        {
            if (_state == GameState.Hunt)
            {
                SwitchPause(false);
            }
        }

        private void SwitchPause(bool isPauseOn)
        {
            if ((_isPause == true) && (isPauseOn == false))
            {
                _isPause = false;
                Time.timeScale = 1.0f;
            }
            else if ((_isPause == false) && (isPauseOn == true))
            {
                _isPause = true;
                Time.timeScale = 0.0f;
            }
        }

        public void RestartHunt()
        {
            if (_state == GameState.Hunt)
            {
                _npcManager.StopNpcSpawn();
                _npcManager.ClearNpc();
                DeactivateCharacterControll();
                _characterController.RemoveCharacter();
                _sceneController.ClearTemporaryObjects();

                _characterController.CreateCharacter();
                ActivateCharacterControll();
                _npcManager.RestartNpcSpawn();
            }
        }

        #endregion

    }
}
