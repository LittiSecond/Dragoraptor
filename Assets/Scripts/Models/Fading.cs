using System;
using UnityEngine;


namespace Dragoraptor
{
    public class Fading : MonoBehaviour, IExecutable
    {

        public event Action OnFadingEnd;

        [SerializeField] private SpriteRenderer _renderer;

        private Color _startColor;
        private Color _endColor;
        private float _fadingDuration = 5.0f;
        private float _startTime;
        private bool _isFading;


        public float FadingDuration { set => _fadingDuration = value; }


        private void Awake()
        {
            _startColor = _renderer.color;
            _endColor = _startColor;
            _endColor.a = 0.0f;
        }

        private void OnDisable()
        {
            Services.Instance.UpdateService.RemoveFromUpdate(this);
            _renderer.color = _startColor;
            _isFading = false;
        }


        public void StartFading()
        {
            _startTime = Time.time;
            Services.Instance.UpdateService.AddToUpdate(this);
            _isFading = true;
        }

        
        #region IExecutable

        public void Execute()
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
    }
}
