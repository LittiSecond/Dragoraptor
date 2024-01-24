using System.Collections.Generic;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class GameController : MonoBehaviour
    {

        [SerializeField] private GamePlaySettings DefaultGamePlaySettings;
        [SerializeField] private Campaign DefoultCampaign;
        private Controllers _controllers;
        private List<IExecutable> _executables;


        private void Start()
        {
            _executables = new List<IExecutable>();
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.SaveAssets();
#endif

            CameraFitter.FitCamera();

            _controllers = new Controllers(DefaultGamePlaySettings);

            Services.Instance.GameProgress.SetCampaign(DefoultCampaign);
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

    }
}