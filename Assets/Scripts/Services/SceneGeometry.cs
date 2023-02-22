using UnityEngine;


namespace Dragoraptor
{
    public sealed class SceneGeometry
    {
        #region Fields

        private Rect _worldWisibleArea;

        private float _screenToWorldRatio;

        private bool _isInitialized;

        #endregion


        #region Methods

        public Vector2 ConvertScreenPositionToWorld(Vector2 screenPositionInPx )
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            Vector2 worldPosition;

            worldPosition.x = _worldWisibleArea.xMin + screenPositionInPx.x * _screenToWorldRatio;
            worldPosition.y = _worldWisibleArea.yMin + screenPositionInPx.y * _screenToWorldRatio;
            return worldPosition;
        }

        private void Initialize()
        {
            Camera camera = Camera.main;

            float worldInCameraHeight = camera.orthographicSize * 2;
            _screenToWorldRatio = worldInCameraHeight / Screen.height;
            float worldInCameraWidth = Screen.width * _screenToWorldRatio;

            Vector2 cameraPos = camera.transform.position;

            float yMin = cameraPos.y - camera.orthographicSize;
            float xMin = cameraPos.x - worldInCameraWidth / 2;

            _worldWisibleArea = new Rect(xMin, yMin, worldInCameraWidth, worldInCameraHeight);

            _isInitialized = true;
        }

        public Rect GetVisibleArea()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
            return _worldWisibleArea;
        }

        #endregion
    }
}
