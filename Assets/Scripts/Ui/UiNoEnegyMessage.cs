using UnityEngine;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public sealed class UiNoEnegyMessage : MonoBehaviour, IExecutable, IMessage 
    {
        #region Fields

        [SerializeField] private Image _mainImage;
        [SerializeField] private float _animationDuration = 2.0f;
        [SerializeField] private float _interval = 0.3f;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _secondColor;

        private float _animationTimer;
        private float _intervalTimer;

        private bool _isAnimation;
        private bool _isStartColor;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _startColor = _mainImage.color;
        }

        #endregion


        #region Methods

        public void Hide()
        {
            _mainImage.enabled = false;
            if (_isAnimation)
            {
                _isAnimation = false;
                Services.Instance.UpdateService.RemoveFromUpdate(this);
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            float deltaTime = Time.deltaTime;

            _intervalTimer += deltaTime;
            if (_intervalTimer >= _interval)
            {
                _intervalTimer = 0.0f;
                _mainImage.color = _isStartColor ? _secondColor : _startColor;
                _isStartColor = !_isStartColor;
            }

            _animationTimer += deltaTime;
            if (_animationTimer >= _animationDuration)
            {
                Hide();
            }
        }

        #endregion


        #region IMessage

        public void ShowMessage()
        {
            if (!_isAnimation)
            {
                _isAnimation = true;
                _mainImage.enabled = true;
                _mainImage.color = _startColor;
                _isStartColor = true;
                Services.Instance.UpdateService.AddToUpdate(this);
                _intervalTimer = 0.0f;
            }
            _animationTimer = 0.0f;
        }

        #endregion
    }
}