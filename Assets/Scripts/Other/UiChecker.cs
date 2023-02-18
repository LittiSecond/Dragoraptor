using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;


namespace Dragoraptor.Ui
{
    public static class UiChecker
    {
        #region Fields

        private static List<RaycastResult> _hitObjects;
        private static PointerEventData _pointerEventData;

        private static bool _isInitialized;

        #endregion


        #region Methods

        public static bool CheckIsUiElement(Vector2 positionInPx)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            _pointerEventData.position = positionInPx;
            EventSystem.current.RaycastAll(_pointerEventData, _hitObjects);
            return _hitObjects.Count > 0;
        }


        private static void Initialize()
        {
            _hitObjects = new List<RaycastResult>();
            _pointerEventData = new PointerEventData(EventSystem.current);
            _isInitialized = true;
        }

        #endregion
    }
}
