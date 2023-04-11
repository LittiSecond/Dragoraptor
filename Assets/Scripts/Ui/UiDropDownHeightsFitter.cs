using UnityEngine;


namespace Dragoraptor.Ui
{
    public sealed class UiDropDownHeightsFitter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RectTransform _template;
        [SerializeField] private RectTransform _content;
        [SerializeField] private RectTransform _item;
        [SerializeField] private float _multipler = 0.5f;

        #endregion


        #region UnityMethods

        private void Start()
        {
            float newHeight = _template.rect.width * _multipler;
            Vector2 offsetMin1 = _content.offsetMin;
            Vector2 offsetMax2 = _item.offsetMax;
            Vector2 offsetMin2 = _item.offsetMin;

            offsetMin1.y = -newHeight;
            _content.offsetMin = offsetMin1;

            offsetMax2.y = newHeight / 2;
            offsetMin2.y = -newHeight / 2;
            _item.offsetMax = offsetMax2;
            _item.offsetMin = offsetMin2;
        }

        #endregion
    }
}