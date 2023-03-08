using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class Fading : MonoBehaviour
    {
        #region Fields

        public event Action OnFadingEnd;

        [SerializeField] private SpriteRenderer _renderer;

        private Color _startColor;
        private Color _endColor;
        private float _fadingDuration = 5.0f;
        private float _startTime;
        private bool _isFading;

        #endregion


        #region Properties

        public float FadibgDuration { set => _fadingDuration = value; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _startColor = _renderer.color;
            _endColor = _startColor;
            _endColor.a = 0.0f;
        }

        private void OnDisable()
        {
            _renderer.color = _startColor;
        }

        private void Update()
        {
            if (_isFading)
            {
                float timepassed = Time.time - _startTime;
                Color newColor = Color.Lerp(_startColor, _endColor, timepassed / _fadingDuration);
                _renderer.color = newColor;
                if (timepassed >= _fadingDuration)
                {
                    _isFading = false;
                    OnFadingEnd?.Invoke();
                }
            }
        }

        #endregion


        #region Methods

        public void StartFading()
        {
            _startTime = Time.time;
            _isFading = true;
        }

        #endregion
    }
}
