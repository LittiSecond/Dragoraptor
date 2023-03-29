using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class HuntScreenBehaviour : BaseScreenBehaviour
    {

        #region Fields

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _breakHuntButton;
        [SerializeField] private GameObject _huntMenu;
        [SerializeField] private UiResourceIndicator _hpIndicator;
        [SerializeField] private UiResourceIndicator _energyIndicator;
        [SerializeField] private UiSatietyIndicator _satietyIndicator;
        [SerializeField] private UiScoreIndicator _scoreIndicator;
        [SerializeField] private UiTimeLeftIndicator _timeLeftIndicator;
        [SerializeField] private UiHuntResultsScreen _huntResultsScreen;
        [SerializeField] private UiNoEnegyMessage _noEnergyMessage;

        private bool _isHuntMenuOpen;
        private bool _isEndHuntScreenOpen;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _continueButton.onClick.AddListener(ContinueButtonClick);
            _huntResultsScreen.AddListeners(GetOutOfTheHuntButtonClick, RestartButtonClick);
            _breakHuntButton.onClick.AddListener(BreakButtonClick);
            HideHuntMenu();
            HideEndHuntScreen();
            _noEnergyMessage.Hide();
        }

        #endregion


        #region Methods

        private void SettingsButtonClick()
        {
            if (_isHuntMenuOpen)
            {
                HideHuntMenu();
                Services.Instance.GameStateManager.OnMenuClosed();
            }
            else
            {
                ShowHuntMenu();
                Services.Instance.GameStateManager.OnMenuOpened();
            }
        }

        private void ContinueButtonClick()
        {
            HideHuntMenu();
            Services.Instance.GameStateManager.OnMenuClosed();
        }

        private void BreakButtonClick()
        {
            HideHuntMenu();
            Services.Instance.GameStateManager.BreakHunt();
        }

        private void GetOutOfTheHuntButtonClick()
        {
            if (_isHuntMenuOpen)
            {
                HideHuntMenu();
            }
            if (_isEndHuntScreenOpen)
            {
                HideEndHuntScreen();
            }
            Services.Instance.GameStateManager.SwitchToMainScreen();
        }

        private void RestartButtonClick()
        {
            HideEndHuntScreen();
            Services.Instance.GameStateManager.RestartHunt();
        }

        private void HideHuntMenu()
        {
            _huntMenu.SetActive(false);
            _isHuntMenuOpen = false;
        }

        private void ShowHuntMenu()
        {
            _huntMenu.SetActive(true);
            _isHuntMenuOpen = true;
        }

        public void SetControllers(IObservableResource playerHealth, EnergyController energyController,
            PlayerSatiety playerSatiety, 
            TimeController timeController, IScoreSource scoreSource, IHuntResultsSource huntResultsSource )
        {
            _hpIndicator.SetSource(playerHealth);
            _energyIndicator.SetSource(energyController);
            energyController.SetNoEnergyMessage(_noEnergyMessage);
            _satietyIndicator.SetSatietySource(playerSatiety);
            timeController.SetTimeView(_timeLeftIndicator);
            scoreSource.OnScoreChanged += _scoreIndicator.SetScore;
            _huntResultsScreen.SetHuntResultsSource(huntResultsSource);
        }

        public void ShowEndHuntScreen()
        {
            _huntResultsScreen.Show();
            _isEndHuntScreenOpen = true;
        }

        private void HideEndHuntScreen()
        {
            _huntResultsScreen.Hide();
            _isEndHuntScreenOpen = false;
        }

        #endregion
    }
}