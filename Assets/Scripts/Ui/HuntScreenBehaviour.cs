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
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _breakDefeatButton;
        [SerializeField] private GameObject _huntMenu;
        [SerializeField] private GameObject _defeatMenu;
        [SerializeField] private UiResourceIndicator _hpIndicator;
        [SerializeField] private UiSatietyIndicator _satietyIndicator;
        [SerializeField] private UiScoreIndicator _scoreIndicator;
        [SerializeField] private UiTimeLeftIndicator _timeLeftIndicator;

        private bool _isDefeatMenuOpen;
        private bool _isHuntMenuOpen;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _continueButton.onClick.AddListener(ContinueButtonClick);
            _breakHuntButton.onClick.AddListener(BreakButtonClick);
            _restartButton.onClick.AddListener(RestartButtonClick);
            _breakDefeatButton.onClick.AddListener(BreakButtonClick);
            HideHuntMenu();
            HideDefeatMenu();
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
            if (_isHuntMenuOpen)
            {
                HideHuntMenu();
            }
            if (_isDefeatMenuOpen)
            {
                HideDefeatMenu();
            }
            Services.Instance.GameStateManager.SwitchToMainScreen();
        }

        private void RestartButtonClick()
        {
            HideDefeatMenu();
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

        private void HideDefeatMenu()
        {
            _defeatMenu.SetActive(false);
            _isDefeatMenuOpen = false;
        }

        public void ShowDefeatMenu()
        {
            _defeatMenu.SetActive(true);
            _isDefeatMenuOpen = true;
        }

        public void SetControllers(PlayerHealth playerHealth, PlayerSatiety playerSatiety, 
            TimeController timeController, IScoreSource scoreSource)
        {
            _hpIndicator.SetSource(playerHealth);
            _satietyIndicator.SetSatietySource(playerSatiety);
            timeController.SetTimeView(_timeLeftIndicator);
            scoreSource.OnScoreChanged += _scoreIndicator.SetScore;
        }

        #endregion
    }
}