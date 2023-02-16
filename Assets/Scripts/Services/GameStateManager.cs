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
            }
        }

        public void SetUiManager(UiManager manager)
        {
            _uiManager = manager;
        }

        #endregion


    }
}
