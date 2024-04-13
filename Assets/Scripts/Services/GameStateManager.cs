using UnityEngine;
using Dragoraptor.Ui;


namespace Dragoraptor
{
    public sealed class GameStateManager
    {
        
        private enum GameState
        {
            None        = 0,
            MainScreen  = 1,
            Hunt        = 2
        }

        private UiManager _uiManager;
        private PlayerCharacterController _characterController;
        private SceneController _sceneController;
        private NpcManager _npcManager;
        private LevelProgressController _levelProgressControler;

        private GameState _state;

        private bool _isPause;


        public void SetControllers(PlayerCharacterController pcc, SceneController sc, NpcManager nm, 
            LevelProgressController lpc)
        {
            _characterController = pcc;
            _sceneController = sc;
            _npcManager = nm;
            _levelProgressControler = lpc;
        }

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwitchToMainScreen();
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
                
                _levelProgressControler.LevelEnd();
                _levelProgressControler.RegistrateHuntResults();

                _npcManager.StopNpcSpawn();
                _npcManager.ClearNpc();
                DeactivateCharacterControl();
                _characterController.RemoveCharacter();
                _sceneController.SetMainScreenScene();
                _uiManager.SwitchToMainScreen();
            }
        }

        public void SwitchToHunt()
        {
            if (_state == GameState.MainScreen)
            {
                _state = GameState.Hunt;
                //Services.Instance.GameProgress.ChooseNextLevel();

                _uiManager.SwitchToHuntScreen();

                _sceneController.BuildLevel();
                _characterController.CreateCharacter();
                ActivateCharacterControl();
                _npcManager.PrepareNpcSpawn();
                _npcManager.RestartNpcSpawn();
                _levelProgressControler.LevelStart();
            }
        }

        private void ActivateCharacterControl()
        {
            _characterController.CharacterControllOn();
        }

        private void DeactivateCharacterControl()
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
                _levelProgressControler.LevelEnd();
                _npcManager.StopNpcSpawn();
                _npcManager.ClearNpc();
                DeactivateCharacterControl();
                _characterController.RemoveCharacter();
                _sceneController.ClearTemporaryObjects();

                _characterController.CreateCharacter();
                ActivateCharacterControl();
                _npcManager.RestartNpcSpawn();
                _levelProgressControler.LevelStart();
                SwitchPause(false);
            }
        }

        public void CharacterKilled()
        {
            _levelProgressControler.LevelEnd();
            Services.Instance.UiFactory.GetHuntScreen().ShowEndHuntScreen();
        }

        public void BreakHunt()
        {
            _levelProgressControler.LevelEnd();
            Services.Instance.UiFactory.GetHuntScreen().ShowEndHuntScreen();
            SwitchPause(true);
        }

    }
}
