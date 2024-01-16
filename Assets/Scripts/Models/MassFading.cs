using System;
using UnityEngine;


namespace Dragoraptor
{
    public class MassFading : MonoBehaviour, IExecutable
    {

        [SerializeField] private SpriteRenderer[] _renderersToFade;
        [SerializeField] private float _fadingDuration = 5.0f;

        private Color[] _startColors;
        private Color[] _endColors;

        private float _startTime;

        private bool _isFading;


        public float FadingDuration { set => _fadingDuration = value; }


        private void Awake()
        {
            int quantity = _renderersToFade.Length;
            _startColors = new Color[quantity];
            _endColors = new Color[quantity];

            for (int i = 0; i < quantity; i++)
            {
                Color color = _renderersToFade[i].color;
                _startColors[i] = color;
                color.a = 0.0f;
                _endColors[i] = color;
            }
        }

        private void OnDisable()
        {
            Services.Instance.UpdateService.RemoveFromUpdate(this);

            for (int i = 0; i < _renderersToFade.Length; i++)
            {
                _renderersToFade[i].color = _startColors[i];
            }

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

                for (int i = 0; i < _renderersToFade.Length; i++)
                {
                    Color newColor = Color.Lerp(_startColors[i], _endColors[i], timepassed / _fadingDuration);
                    _renderersToFade[i].color = newColor;
                }

                if (timepassed >= _fadingDuration)
                {
                    _isFading = false;
                }
            }
        }

        #endregion
    }
}
