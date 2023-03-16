using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiSatietyIndicator : UiResourceIndicator
    {
        #region Fields

        [SerializeField] private RectTransform _marker;
        [SerializeField] private RectTransform _parentOfMarker;

        #endregion


        #region Methods

        public void SetSatietyThreshold(float relativeMaxValue)
        {
            float xOffset = -_parentOfMarker.rect.width * (1.0f - relativeMaxValue);
            Vector2 offsetMax = _marker.offsetMax;
            Vector2 offsetMin = _marker.offsetMin;
            offsetMax.x = xOffset;
            offsetMin.x = xOffset;
            _marker.offsetMax = offsetMax;
            _marker.offsetMin = offsetMin;
        }

        #endregion

    }
}
