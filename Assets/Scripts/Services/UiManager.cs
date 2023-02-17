using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiManager
    {

        #region Fields

        private BaseScreenBehaviour _currentScreen;

        private UiFactory _uiFactory;
        private MainScreenBehaviour _mainScreen;
        private HuntScreenBehaviour _huntScreen;

        private bool _haveMainScreen;
        private bool _haveHuntScreen;

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
            if (!_haveMainScreen)
            {
                _mainScreen = _uiFactory.GetMainScreen();
                _haveMainScreen = true;
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
            if (!_haveHuntScreen)
            {
                _huntScreen = _uiFactory.GetHuntScreen();
                _haveHuntScreen = true;
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
