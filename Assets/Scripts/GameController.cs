using UnityEngine;


namespace Dragoraptor
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GamePlaySettings DefaultGamePlaySettings;
        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.SaveAssets();
#endif

            CameraFitter.FitCamera();

            _controllers = new Controllers(DefaultGamePlaySettings);

            Services.Instance.GameStateManager.SetMainScreenAtStartGame();
        }


        private void Update()
        {
            for (int i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        #endregion

    }
}