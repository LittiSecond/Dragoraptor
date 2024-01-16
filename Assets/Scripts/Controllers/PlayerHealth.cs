using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerHealth : ITakeDamage, IBodyUser, IObservableResource
    {

        public event Action OnHealthEnd;

        private int _maxHealth;
        private int _health;
        private int _armor;

        
        public PlayerHealth(GamePlaySettings gps)
        {
            _maxHealth = gps.MaxHealth;
            _armor = gps.Armor;
        }


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


        #region ITakeDamag

        public void TakeDamage(int amount)
        {
            amount -= _armor;
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
            body.SetDamageReceiver(this);
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
