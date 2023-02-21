using UnityEngine;


namespace Dragoraptor
{
    sealed class TrajectoryPainter
    {

        #region Fields

        private Transform _bodyTransform;
        private LineRenderer _lineRenderer;

        private Vector2 _bodyPosition;
        private Vector2 _touchPosition;

        private float _maxLength;
        private float _length = 3.0f;

        #endregion


        //#region ClassLifeCycles

        //public TrajectoryPainter(GamePlaySettings gamePlaySettings)
        //{
        //}

        //#endregion


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

            secondPoint.Normalize();
            secondPoint *= -_length;

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
