using System;


namespace Dragoraptor
{
    public sealed class PlayerSatiety : IObservableResource
    {

        public event Action OnVictorySatietyReached;
        public event Action<float> OnVictorySatietyChanged;
        public event Action OnMaxSatietyReached;

        private int _maxSatiety;
        private int _satiety;
        private float _victorySatiety;


        public PlayerSatiety(GamePlaySettings gps)
        {
            _maxSatiety = gps.MaxSatiety;
        }


        public void ResetSatiety()
        {
            _satiety = 0;
            OnValueChanged?.Invoke(_satiety);
        }

        public void SetMaxSatiety(int maxSatiety)
        {
            _maxSatiety = maxSatiety;
            OnMaxValueChanged?.Invoke(_maxSatiety);
        }

        public void SetVictorySatiety(float satietyRelativeMax)
        {
            _victorySatiety = satietyRelativeMax;
            OnVictorySatietyChanged?.Invoke(_victorySatiety);
        }

        public void AddSatiety(int additionalSatiety)
        {
            if (additionalSatiety > 0 && _satiety < _maxSatiety)
            {
                _satiety += additionalSatiety;
                if (_satiety >= _maxSatiety)
                {
                    _satiety = _maxSatiety;
                    OnMaxSatietyReached?.Invoke();
                }
                OnValueChanged?.Invoke(_satiety);

                if (_satiety >= _victorySatiety * _maxSatiety)
                {
                    OnVictorySatietyReached?.Invoke();
                }
            }
        }


        #region IObservableResource

        public int MaxValue { get => _maxSatiety; }

        public int Value { get => _satiety; }

        public event Action<int> OnMaxValueChanged;
        public event Action<int> OnValueChanged;

        #endregion
    }
}
