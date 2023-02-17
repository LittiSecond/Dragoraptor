using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiManager
    {

        #region Fields

        private UiFactory _uiFactory;
        private IScreenBehaviour _currentScreen;
        private IScreenBehaviour _mainScreen;
        private IScreenBehaviour _huntScreen;

        #endregion


        #region ClassLifeCycles

        public UiManager()
        {
            _uiFactory = new UiFactory();
        }

        #endregion


        #region Methods

        public void SwichToMainScreen()
        {
            if (_mainScreen == null)
            {
                _mainScreen = _uiFactory.GetMainScreen();
            }

            if (_currentScreen != _mainScreen)
            {
                _currentScreen?.Hide();
                _currentScreen = _mainScreen;
                _currentScreen.Show();
            }
        }

        public void SwichToHuntScreen()
        {
            if (_huntScreen == null)
            {
                _huntScreen = _uiFactory.GetHuntScreen();
            }

            if (_currentScreen != _huntScreen)
            {
                _currentScreen?.Hide();
                _currentScreen = _huntScreen;
                _currentScreen.Show();
            }
        }


        #endregion
    }
}
