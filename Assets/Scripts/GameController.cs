using UnityEngine;


namespace Dragoraptor
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            CameraFitter.FitCamera();

            _controllers = new Controllers();

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