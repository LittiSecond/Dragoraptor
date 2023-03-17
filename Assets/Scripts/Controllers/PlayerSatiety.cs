using System;
using UnityEngine;

using Dragoraptor.Ui;

namespace Dragoraptor
{
    public sealed class PlayerSatiety : IObservableResource, IOnceInitializable
    {
        #region Fields

        public event Action OnVictorySatietyReached;

        private UiSatietyIndicator _satietyIndicator;

        private int _maxSatiety;
        private int _satiety;
        private int _victorySatiety;

        #endregion


        #region ClassLifeCycles

        public PlayerSatiety(GamePlaySettings gps)
        {
            _maxSatiety = gps.MaxSatiety;
        }

        #endregion


        #region Methods

        public void ResetSetiety()
        {
            _satiety = 0;
            OnValueChanged?.Invoke(_satiety);
        }

        public void SetMaxSatiety(int maxSatiety)
        {
            _maxSatiety = maxSatiety;
            OnMaxValueChanged?.Invoke(_maxSatiety);
        }

        public void SetVictorySatiety(int satiety)
        {
            _victorySatiety = satiety;
        }

        public void AddSatiety(int additionalSatiety)
        {
            if (additionalSatiety > 0 && _satiety < _maxSatiety)
            {
                _satiety += additionalSatiety;
                if (_satiety > _maxSatiety)
                {
                    _satiety = _maxSatiety;
                }
                OnValueChanged?.Invoke(_satiety);

                if (_satiety >= _victorySatiety)
                {
                    OnVictorySatietyReached?.Invoke();
                }
            }
        }

        #endregion


        #region IObservableResource

        public int MaxValue { get => _maxSatiety; }

        public int Value { get => _satiety; }

        public event Action<int> OnMaxValueChanged;
        public event Action<int> OnValueChanged;

        #endregion


        #region IOnceInitializable

        public void OnceInitialize()
        {
            _satietyIndicator = Services.Instance.UiFactory.GetSatietyIndicator();
            _satietyIndicator.SetSource(this);
            _satietyIndicator.SetSatietyThreshold(0.75f);
        }

        #endregion
    }
}
