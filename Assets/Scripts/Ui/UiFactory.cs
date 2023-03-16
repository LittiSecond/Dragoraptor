using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiFactory
    {

        #region Fields

        private const string HUNT_SCREEN_PREFAB_ID = "HuntScreen";
        private const string MAIN_SCREEN_PREFAB_ID = "MainScreen";

        private Transform _canvas;
        private MainScreenBehaviour _mainScreen;
        private HuntScreenBehaviour _huntScreen;


        #endregion


        #region ClassLifeCycles

        public UiFactory()
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            _canvas = canvas.transform;
        }

        #endregion


        #region Methods

        public MainScreenBehaviour GetMainScreen()
        {
            if (_mainScreen == null)
            {
                GameObject prefab = PrefabLoader.GetPrefab(MAIN_SCREEN_PREFAB_ID);
                if (prefab)
                {
                    var go = UnityEngine.Object.Instantiate(prefab, _canvas);
                    _mainScreen = go.GetComponent<MainScreenBehaviour>();
                }
            }
            return _mainScreen;
        }

        public HuntScreenBehaviour GetHuntScreen()
        {
            if (_huntScreen == null)
            {
                GameObject prefab = PrefabLoader.GetPrefab(HUNT_SCREEN_PREFAB_ID);
                if (prefab)
                {
                    var go = UnityEngine.Object.Instantiate(prefab, _canvas);
                    _huntScreen = go.GetComponent<HuntScreenBehaviour>();
                }
            }
            return _huntScreen;
        }

        public UiResourceIndicator GetHpIndicator()
        {
            if (_huntScreen == null)
            {
                GetHuntScreen();
            }

            return _huntScreen.GetHpIndicator();
        }

        public UiSatietyIndicator GetSatietyIndicator()
        {
            if (_huntScreen == null)
            {
                GetHuntScreen();
            }

            return _huntScreen.GetSatietyIndicator();
        }

        #endregion

    }
}
