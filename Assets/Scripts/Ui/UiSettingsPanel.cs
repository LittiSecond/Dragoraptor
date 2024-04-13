using System;
using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class UiSettingsPanel : BaseScreenBehaviour
    {
        [SerializeField] private Button _closeButton;

        public event Action OnCloseButtonClick;
            

        private void Awake()
        {
            _closeButton.onClick.AddListener((() => OnCloseButtonClick?.Invoke()));
        }
    }
}