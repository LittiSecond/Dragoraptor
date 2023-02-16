using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class MainScreenBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _goHuntingButton;

        private bool _isActive;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
            _goHuntingButton.onClick.AddListener(GoHuntButtonClick);

            _isActive = gameObject.activeSelf;
        }

        #endregion


        #region Methods

        private void SettingsButtonClick()
        {
            Debug.Log("MainScreenBehaviour->SettingsButtonClick:");
        }

        private void GoHuntButtonClick()
        {
            Debug.Log("MainScreenBehaviour->GoHuntButtonClick:");
        }

        public void On()
        {
            if (!_isActive)
            {
                _isActive = true;
                gameObject.SetActive(true);
            }
        }

        public void Off()
        {
            if (_isActive)
            {
                _isActive = false;
                gameObject.SetActive(false);
            }
        }

        #endregion

    }
}