using UnityEngine;


namespace Dragoraptor
{
    sealed class TrajectoryPainter : BaseLinePainter
    {

        #region Fields

        private float _length = 3.0f;

        #endregion


        #region Methods

        public override void Execute()
        {
            Vector2 secondPoint = _touchPosition - _bodyPosition;

            secondPoint.Normalize();
            secondPoint *= -_length;

            _lineRenderer.SetPosition(1, (Vector3)secondPoint);
        }

        #endregion
    }
}
