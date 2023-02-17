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

        private GameState _state;

        #endregion


        #region Properties




        #endregion


        #region ClassLifeCycles

        #endregion


        #region Methods

        public void SetMainScreenAtStartGame()
        {
            if (_state == GameState.None)
            {
                _uiManager.SwichToMainScreen();
                _state = GameState.MainScreen;
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


        #endregion


    }
}
