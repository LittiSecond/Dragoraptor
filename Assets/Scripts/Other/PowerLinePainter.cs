using UnityEngine;


namespace Dragoraptor
{
    sealed class PowerLinePainter
    {
        #region Fields

        private Transform _bodyTransform;
        private LineRenderer _lineRenderer;

        private Vector2 _bodyPosition;
        private Vector2 _touchPosition;

        private float _maxLength;

        #endregion


        #region ClassLifeCycles

        public PowerLinePainter(GamePlaySettings gamePlaySettings)
        {
            _maxLength = gamePlaySettings.MaxJumpPowerIndicatorLength;
        }

        #endregion


        #region Methods

        public void SetData(Transform bodyTransform, LineRenderer lineRenderer) 
        {
            _bodyTransform = bodyTransform;
            _lineRenderer = lineRenderer;
            _lineRenderer.enabled = false;
        }

        public void ClearData()
        {
            _bodyTransform = null;
            _lineRenderer = null;
        }

        public void SetTouchPosition(Vector2 position)
        {
            _touchPosition = position;
        }

        public void Execute()
        {
            Vector2 secondPoint = _touchPosition - _bodyPosition;

            if (secondPoint.sqrMagnitude > _maxLength * _maxLength)
            {
                secondPoint.Normalize();
                secondPoint *= _maxLength;
            }

            _lineRenderer.SetPosition(1, (Vector3)secondPoint);
        }

        public void DrawingOn()
        {
            _lineRenderer.SetPosition(1, Vector3.zero);
            _lineRenderer.enabled = true;
            _bodyPosition = _bodyTransform.position;
        }

        public void DrawingOff()
        {
            _lineRenderer.enabled = false;
        }

        #endregion
    }
}
