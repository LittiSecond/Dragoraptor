using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiFactory
    {

        private const string HUNT_SCREEN_PREFAB_ID = "HuntScreen";
        private const string MAIN_SCREEN_PREFAB_ID = "MainScreen";

        private Transform _canvas;
        private MainScreenBehaviour _mainScreen;
        private HuntScreenBehaviour _huntScreen;


        public UiFactory()
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            _canvas = canvas.transform;
        }


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
                    _huntScreen.Hide();
                }
            }
            return _huntScreen;
        }

    }
}
