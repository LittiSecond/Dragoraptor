using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpCalculator
    {
        #region Fields

        private float _minJumpForce;
        private float _maxJumpForce;
        //      distances betwin _bodyTransform and touch point
        private float _cancelJumpDistance;
        private float _cancelJumpSqrDistance;
        private float _maxJumpForceDistance;

        // force calculation data: Force = k*distance + b
        private float _k;
        private float _b;


        #endregion


        #region ClassLifeCycles

        public JumpCalculator(GamePlaySettings gps)
        {
            _minJumpForce = gps.MinJumpForce;
            _maxJumpForce = gps.MaxJumpForce;
            _cancelJumpDistance = gps.NoJumpPowerIndicatorLength;
            _cancelJumpSqrDistance = _cancelJumpDistance * _cancelJumpDistance;
            _maxJumpForceDistance = gps.MaxJumpPowerIndicatorLength;

            CalculateJumpForceCalcData();
        }

        #endregion


        #region Methods

        private void CalculateJumpForceCalcData()
        {
            _k = (_maxJumpForce - _minJumpForce) / (_maxJumpForceDistance - _cancelJumpDistance);
            _b = _maxJumpForce - _k * _maxJumpForceDistance;
        }

        public Vector2 CalculateJampImpulse(Vector2 jumpDirection)
        {
            Vector2 impulse = Vector2.zero;

            float sqrDistance = jumpDirection.sqrMagnitude;

            if (sqrDistance > _cancelJumpSqrDistance)
            {
                if (CheckIsJumpDirectionGood(jumpDirection))
                {
                    jumpDirection.Normalize();

                    float distance = Mathf.Sqrt(sqrDistance);
                    if (distance > _maxJumpForceDistance)
                    {
                        distance = _maxJumpForceDistance;
                    }
                    float jumpForce = CalculateJumpForce(distance);
                    impulse = jumpDirection * jumpForce;
                }
            }

            return impulse;
        }

        private bool CheckIsJumpDirectionGood(Vector2 direction)
        {
            return direction.y > 0.0f;
        }

        private float CalculateJumpForce(float distance)
        {
            return _k * distance + _b;
        }

        #endregion

    }
}
