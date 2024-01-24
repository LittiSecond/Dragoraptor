using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class MainScreenBehaviour : BaseScreenBehaviour
    {

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _goHuntingButton;
        [SerializeField] private UiGameStatisticsPanel _gameStatisticPanel;


        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _goHuntingButton.onClick.AddListener(GoHuntButtonClick);
        }


        private void SettingsButtonClick()
        {
            Debug.Log("MainScreenBehaviour->SettingsButtonClick:");
        }

        private void GoHuntButtonClick()
        {
            Services.Instance.GameStateManager.SwitchToHunt();
        }

        public override void Show()
        {
            base.Show();
            _gameStatisticPanel.SetProgressData(Services.Instance.GameProgress.GetProgressData());
        }

    }
}