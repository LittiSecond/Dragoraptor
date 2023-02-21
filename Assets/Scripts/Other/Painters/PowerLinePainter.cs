using UnityEngine;


namespace Dragoraptor
{
    sealed class PowerLinePainter : BaseLinePainter
    {
        #region Fields

        private float _maxLength;

        #endregion


        #region ClassLifeCycles

        public PowerLinePainter(GamePlaySettings gamePlaySettings)
        {
            _maxLength = gamePlaySettings.MaxJumpPowerIndicatorLength;
        }

        #endregion


        #region Methods

        public override void Execute()
        {
            Vector2 secondPoint = _touchPosition - _bodyPosition;

            if (secondPoint.sqrMagnitude > _maxLength * _maxLength)
            {
                secondPoint.Normalize();
                secondPoint *= _maxLength;
            }

            _lineRenderer.SetPosition(1, (Vector3)secondPoint);
        }

        #endregion
    }
}
