using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class MainScreenBehaviour : BaseScreenBehaviour
    {
        #region Fields

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _goHuntingButton;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _goHuntingButton.onClick.AddListener(GoHuntButtonClick);
        }

        #endregion


        #region Methods

        private void SettingsButtonClick()
        {
            Debug.Log("MainScreenBehaviour->SettingsButtonClick:");
        }

        private void GoHuntButtonClick()
        {
            Services.Instance.GameStateManager.SwitchToHunt();
        }

        #endregion

    }
}