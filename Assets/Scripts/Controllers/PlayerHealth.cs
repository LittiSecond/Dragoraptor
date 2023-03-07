﻿using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerHealth : ITakeDamag, IBodyUser, IObservableResource
    {

        #region Fields

        public event Action OnHealthEnd;

        private int _maxHealth;
        private int _health;

        private bool _isUiConnected;

        #endregion


        #region ClassLifeCycles

        public PlayerHealth(GamePlaySettings gps)
        {
            _maxHealth = gps.MaxHealth;
        }

        #endregion


        #region Methods

        public void ResetHealth()
        {
            _health = _maxHealth;
            OnValueChanged?.Invoke(_health);
        }

        public void SetMaxHealth(int newMaxHealth )
        {
            _maxHealth = newMaxHealth;
            OnMaxValueChanged?.Invoke(_maxHealth);
        }

        #endregion


        #region ITakeDamag

        public void TakeDamage(int amount)
        {
            if (amount > 0)
            {
                _health -= amount;
                if (_health < 0)
                {
                    _health = 0;
                }
                OnValueChanged?.Invoke(_health);

                if (_health == 0)
                {
                    OnHealthEnd?.Invoke();
                }
            }
        }

        #endregion


        #region IBodyUser

        public void SetBody(PlayerBody body)
        {
            body.SetDamagReceiver(this);
            if (!_isUiConnected)
            {
                _isUiConnected = true;
                Services.Instance.UiFactory.GetHpIndicator().SetSource(this);
            }
        }

        public void ClearBody()
        {
            
        }

        #endregion


        #region IObservableResource

        public event Action<int> OnMaxValueChanged;

        public event Action<int> OnValueChanged;

        public int MaxValue { get => _maxHealth; }

        public int Value { get => _health; }

        #endregion


    }
}