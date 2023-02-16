using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiManager
    {

        #region Fields

        private UiFactory _uiFactory;
        private MainScreenBehaviour _mainScreen;

        private bool _haveMainScreen;

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
            _mainScreen.Show();
        }

        public void SwichToHuntScreen()
        {
            Debug.Log("UiManager->SwichToHuntScreen");
        }






        #endregion
    }
}
