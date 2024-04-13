using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class MainScreenBehaviour : BaseScreenBehaviour
    {

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _goHuntingButton;
        [SerializeField] private Button _statisticButon;
        [SerializeField] private UiGameStatisticsPanel _gameStatisticPanel;
        [SerializeField] private UiLevelsMap _levelsMap;

        private UiSettingsPanel _settingsPanel;

        private IScreenBehaviour _currentPanel;

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _goHuntingButton.onClick.AddListener(GoHuntButtonClick);
            _statisticButon.onClick.AddListener(StatisticButtonClick);
            Services.Instance.GameProgress.SetLevelMap(_levelsMap);
            
            _settingsPanel = Services.Instance.UiFactory.GetSettingsPanel();
            _settingsPanel.OnCloseButtonClick += ClosePanelButtonClick;
            
            _gameStatisticPanel.Hide();
            _gameStatisticPanel.OnCloseButtonClick += ClosePanelButtonClick;
            
            _levelsMap.Hide();
            _levelsMap.OnCloseButtonClick += ClosePanelButtonClick;
        }


        private void SettingsButtonClick()
        {
            //Debug.Log("MainScreenBehaviour->SettingsButtonClick:");

            if (_currentPanel != _settingsPanel as IScreenBehaviour )
            {
                _currentPanel?.Hide();
                _settingsPanel.Show();
                _currentPanel = _settingsPanel;
            }
        }

        private void GoHuntButtonClick()
        {
            //Services.Instance.GameStateManager.SwitchToHunt();
            if (_currentPanel != _levelsMap as IScreenBehaviour )
            {
                _currentPanel?.Hide();
                _levelsMap.Show();
                _currentPanel = _levelsMap;
            }
        }

        private void StatisticButtonClick()
        {
            if (_currentPanel != _gameStatisticPanel as IScreenBehaviour )
            {
                _currentPanel?.Hide();
                _gameStatisticPanel.Show();
                _currentPanel = _gameStatisticPanel;
            }
        }

        public override void Show()
        {
            base.Show();
            _gameStatisticPanel.SetProgressData(Services.Instance.GameProgress.GetProgressData());
        }

        private void ClosePanelButtonClick()
        {
            _currentPanel?.Hide();
            _currentPanel = null;
        }

    }
}