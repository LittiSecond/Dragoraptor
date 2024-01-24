using UnityEngine;


namespace Dragoraptor
{
    public static class CameraFitter
    {
        
        private const float GAME_AREA_WIDTH = 6.0f;

        public static void FitCamera()
        {
            float screenRatio = (float)Screen.height / Screen.width;
            float areaHeigth = GAME_AREA_WIDTH * screenRatio;
            Camera.main.orthographicSize = areaHeigth / 2;
        }

    }
}
