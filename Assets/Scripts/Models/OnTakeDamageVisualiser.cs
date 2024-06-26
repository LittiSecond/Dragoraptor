﻿using UnityEngine;
using UnityEngine.UI;

namespace Dragoraptor
{
    public class OnTakeDamageVisualiser : IExecutable 
    {
        private enum FadeState { None, Up, Down};
        
        private Image _fadedImage;
        private float _max = 0.9f;
        private float _min = 0.4f;
        private float _upTime = 0.2f;
        private float _downTime = 0.5f;
        
        private FadeState _state;
        private Color _currentColor;
        private float _timeCounter;
        private float _targetAlpha;
        private float _beginAlpha;
        

        public OnTakeDamageVisualiser(PlayerHealth ph)
        {
            ph.OnDamaged += Damaged;
            var fader = Services.Instance.UiFactory.GetDamageFader();
            _fadedImage = fader.GetComponent<Image>();
            _currentColor = _fadedImage.color;
            _state = FadeState.None;
        }


        private void Damaged()
        {
            Fade(1.0f);
        }
        
        #region IExecutable

        public void Execute()
        {
            switch (_state)
            {
                case FadeState.Up:
                    UpAlpha();
                    break;
                case FadeState.Down:
                    DownAlpha();
                    break;
            }
        }

        #endregion

        private void UpAlpha()
        {
            _timeCounter += Time.deltaTime;
            float alpha = Mathf.Clamp( _timeCounter / _upTime, 0, 1) * (_targetAlpha - _beginAlpha) + _beginAlpha;
            _currentColor.a = alpha;
            _fadedImage.color = _currentColor;
            if (_timeCounter >= _upTime)
            {
                _state = FadeState.Down;
                _timeCounter = 0;
            }
        }

        private void DownAlpha()
        {
            _timeCounter += Time.deltaTime;
            float alpha = Mathf.Clamp(1 - _timeCounter / _downTime, 0, 1) * _targetAlpha;
            _currentColor.a = alpha;
            _fadedImage.color = _currentColor;
            if (_timeCounter >= _downTime)
            {
                _state = FadeState.None;
                _timeCounter = 0;
            }
        }

        public void Fade(float value)
        {
            switch (_state)
            {
            case FadeState.None:
                _targetAlpha = Mathf.Clamp(value, _min, _max);
                _timeCounter = 0;
                _beginAlpha = 0;
                _state = FadeState.Up;
            break;
            case FadeState.Up:
                if (value <= _targetAlpha)
                {
                    break;
                }
                _targetAlpha = Mathf.Clamp(value, _min, _max);
                break;
            case FadeState.Down:
                float targetAlpha = Mathf.Clamp(value, _min, _max);
                float a = _currentColor.a;
                if (targetAlpha <= a)
                {
                    break;
                }
                _timeCounter = 0;
                _beginAlpha = a;
                _targetAlpha = targetAlpha;
                _state = FadeState.Up;
                break;
            }
        }
        
    }
}