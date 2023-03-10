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
        [SerializeField] private GameObject _menu;
        [SerializeField] private UiResourceIndicator _hpIndicator;


        private bool _isMenuOpen;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _continueButton.onClick.AddListener(ContinueButtonClick);
            _breakHuntButton.onClick.AddListener(BreakButtonClick);
            HideMenu();
        }

        #endregion


        #region Methods

        private void SettingsButtonClick()
        {
            if (_isMenuOpen)
            {
                HideMenu();
                Services.Instance.GameStateManager.OnMenuClosed();
            }
            else
            {
                ShowMenu();
                Services.Instance.GameStateManager.OnMenuOpened();
            }
        }

        private void ContinueButtonClick()
        {
            HideMenu();
        }

        private void BreakButtonClick()
        {
            HideMenu();
            Services.Instance.GameStateManager.SwitchToMainScreen();
        }

        private void HideMenu()
        {
            _menu.SetActive(false);
            _isMenuOpen = false;
        }

        private void ShowMenu()
        {
            _menu.SetActive(true);
            _isMenuOpen = true;
        }

        public UiResourceIndicator GetHpIndicator()
        {
            return _hpIndicator;
        }

        #endregion
    }
}