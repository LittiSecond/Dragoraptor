namespace Dragoraptor.Ui
{
    public sealed class UiManager
    {

        private IScreenBehaviour _currentScreen;
        private IScreenBehaviour _mainScreen;
        private IScreenBehaviour _huntScreen;


        public void SwitchToMainScreen()
        {
            if (_mainScreen == null)
            {
                _mainScreen = Services.Instance.UiFactory.GetMainScreen();
            }

            if (_currentScreen != _mainScreen)
            {
                _currentScreen?.Hide();
                _currentScreen = _mainScreen;
                _currentScreen.Show();
            }
        }

        public void SwitchToHuntScreen()
        {
            if (_huntScreen == null)
            {
                _huntScreen = Services.Instance.UiFactory.GetHuntScreen();
            }

            if (_currentScreen != _huntScreen)
            {
                _currentScreen?.Hide();
                _currentScreen = _huntScreen;
                _currentScreen.Show();
            }
        }

    }
}
