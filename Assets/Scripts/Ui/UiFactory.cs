using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiFactory
    {

        #region Fields

        private const string MAIN_SCREEN_PREFAB_ID = "MainScreen";

        private Transform _canvas;
        private MainScreenBehaviour _mainScreen;


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
                    var go = UnityEngine.Object.Instantiate<GameObject>(prefab, _canvas);
                    _mainScreen = go.GetComponent<MainScreenBehaviour>();
                }
            }
            return _mainScreen;
        }


        #endregion

    }
}
