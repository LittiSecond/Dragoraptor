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
        private TouchInputController _touchInputController;

        private GameState _state;

        #endregion


        #region Methods

        public void SetTouchInputController(TouchInputController tic) => _touchInputController = tic;

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;

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
            _touchInputController.On();
        }

        private void DeactivateCharacterControll()
        {
            _touchInputController.Off();
        }


        #endregion


    }
}
