using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class HpIndicator : MonoBehaviour
    {

        [SerializeField] private Transform _barTransform;
        [SerializeField] private GameObject _root;
        private IHealth _healthSource;
        private float _maxXScale;
        private int _maxHealth;

        private bool _isVisible;


        private void Awake()
        {
            _maxXScale = _barTransform.localScale.x;
            _isVisible = _root.activeSelf;
        }


        public void SetIHealth(IHealth health)
        {
            if (_healthSource == null)
            {
                _healthSource = health;
                _healthSource.OnHealthChanged += SetValue;
                UpdateValues();
            }
        }

        private void SetValue(int newHealth)
        {
            if (_maxHealth > 0.0f)
            {
                float scale = (float)newHealth / _maxHealth;
                Vector3 localScale = _barTransform.localScale;
                localScale.x = scale * _maxXScale;
                _barTransform.localScale = localScale;

                if (newHealth < _maxHealth && !_isVisible)
                {
                    Show();
                }
                else if ( newHealth == _maxHealth && _isVisible )
                {
                    Hide();
                }
            }
        }

        public void UpdateValues()
        {
            if (_healthSource != null)
            {
                _maxHealth = _healthSource.MaxHealth;
                SetValue(_healthSource.Health);
            }
        }

        private void Show()
        {
            _root.SetActive(true);
            _isVisible = true;
        }

        private void Hide()
        {
            _root.SetActive(false);
            _isVisible = false;
        }

    }
}