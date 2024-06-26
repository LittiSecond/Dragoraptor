﻿using System;


namespace Dragoraptor
{
    public sealed class NpcHealth : ITakeDamage, IInitializable, IHealth
    {

        public event Action<int> OnHealthChanged;
        public event Action OnHealthEnd;

        private IDamageObserver _damageObserver;

        private int _maxHealth;
        private int _currentHealth;
        private int _armor;


        public int Health { get => _currentHealth; }

        public int MaxHealth { get => _maxHealth; }


        public NpcHealth(int maxHealth, int armor)
        {
            _maxHealth = maxHealth;
            _armor = armor;
        }


        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void SetDamageObserver(IDamageObserver observer)
        {
            _damageObserver = observer;
        }


        #region ITakeDamag

        public void TakeDamage(int amount)
        {
            amount -= _armor;
            if (amount > 0)
            {
                _damageObserver?.OnDamaged(amount);
                _currentHealth -= amount;
                if (_currentHealth < 0)
                {
                    _currentHealth = 0;
                }
                OnHealthChanged?.Invoke(_currentHealth);

                if (_currentHealth == 0)
                {
                    OnHealthEnd?.Invoke();
                }
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            ResetHealth();
        }

        #endregion

    }
}
