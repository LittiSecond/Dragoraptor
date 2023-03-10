using System.Collections.Generic;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GamePlaySettings DefaultGamePlaySettings;
        private Controllers _controllers;
        private List<IExecutable> _executables;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _executables = new List<IExecutable>();
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.SaveAssets();
#endif

            CameraFitter.FitCamera();

            _controllers = new Controllers(DefaultGamePlaySettings);

            Services.Instance.GameStateManager.SetMainScreenAtStartGame();
            Services.Instance.UpdateService.SetListToExecute(_executables);
        }


        private void Update()
        {
            for (int i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }

            for (int i = 0; i < _executables.Count; i++)
            {
                _executables[i].Execute();
            }
        }

        #endregion

    }
}