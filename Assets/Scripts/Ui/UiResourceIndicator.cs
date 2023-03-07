using UnityEngine;


namespace Dragoraptor.Ui
{
    public class UiResourceIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RectTransform _bar;

        private IObservableResource _source;

        private int _maxValue;
        private int _value;

        #endregion


        #region Methods

        public void SetSource(IObservableResource source)
        {
            _source = source;
            _source.OnMaxValueChanged += OnMaxValueChanged;
            _source.OnValueChanged += OnValueChanged;
            _maxValue = _source.MaxValue;
            _value = _source.Value;
            UpdateValues();
        }

        private void UpdateValues()
        {
            Vector3 scale = _bar.localScale;
            scale.x = (float)_value / _maxValue;
            _bar.localScale = scale;
        }

        private void OnValueChanged(int newValue)
        {
            _value = newValue;
            UpdateValues();
        }

        private void OnMaxValueChanged(int newMaxValue)
        {
            _maxValue = newMaxValue;
            UpdateValues();
        }

        #endregion
    }
}