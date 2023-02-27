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

        private GameState _state;

        #endregion


        #region Methods

        public void SetCharacterController(PlayerCharacterController pcc) => _characterController = pcc;

        public void SetControllers(PlayerCharacterController pcc, SceneController sc)
        {
            _characterController = pcc;
            _sceneController = sc;
        }

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;

                _characterController.CreateCharacter();
                ActivateCharacterControll();
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
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;
            }
        }

        public void SwitchToHunt()
        {
            if (_state == GameState.MainScreen)
            {
                _uiManager.SwichToHuntScreen();
                _state = GameState.Hunt;
            }
        }

        private void ActivateCharacterControll()
        {
            _characterController.CharacterControllOn();
        }

        private void DeactivateCharacterControll()
        {
            _characterController.CharacterControllOff();
        }


        #endregion


    }
}
