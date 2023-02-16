using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class HuntScreenBehaviour : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Button _settingsButton;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settingsButton.onClick.AddListener(SettingsButtonClick);
        }

        #endregion


        #region Methods

        private void SettingsButtonClick()
        {
            Debug.Log("HuntScreenBehaviour->SettingsButtonClick:");
        }


        #endregion



    }
}