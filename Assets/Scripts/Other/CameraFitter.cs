using UnityEngine;


namespace Dragoraptor
{
    public static class CameraFitter
    {
        #region Fields

        private const float GAME_AREA_WIDTH = 6.0f;

        #endregion


        #region Methods

        public static void FitCamera()
        {
            float screenRatio = (float)Screen.height / Screen.width;
            float areaHeigth = GAME_AREA_WIDTH * screenRatio;
            Camera.main.orthographicSize = areaHeigth / 2;
        }

        #endregion
    }
}
