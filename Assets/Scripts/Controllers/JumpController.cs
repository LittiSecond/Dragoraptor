using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpController
    {
        #region Fields

        private Transform _bodyTransform;
        private Rigidbody2D _rigidbody;

        private readonly CharacterStateHolder _stateHolder;

        private float _minJumpForce;
        private float _maxJumpForce;
        //      distances betwin _bodyTransform and touch point
        private float _cancelJumpDistance;
        private float _cancelJumpSqrDistance;
        private float _maxJumpForceDistance;

        // force calculation data: Force = k*distance + b
        private float _k;
        private float _b;

        private CharacterState _state;

        private bool _isEnabled;

        #endregion


        #region ClassLifeCycles

        public JumpController(CharacterStateHolder csh, GamePlaySettings gamePlaySettings)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;

            _minJumpForce = gamePlaySettings.MinJumpForce;
            _maxJumpForce = gamePlaySettings.MaxJumpForce;
            _cancelJumpDistance = gamePlaySettings.NoJumpPowerIndicatorLength;
            _cancelJumpSqrDistance = _cancelJumpDistance * _cancelJumpDistance;
            _maxJumpForceDistance = gamePlaySettings.MaxJumpPowerIndicatorLength;

            CalculateJumpForceCalcData();
        }

        #endregion


        #region Methods

        private void CalculateJumpForceCalcData()
        {
            _k = (_maxJumpForce - _minJumpForce) / (_maxJumpForceDistance - _cancelJumpDistance);
            _b = _maxJumpForce - _k * _maxJumpForceDistance;
        }

        public void SetBody(PlayerBody pb)
        {
            if (pb)
            {
                _bodyTransform = pb.transform;
                _rigidbody = pb.GetRigidbody();
                _isEnabled = true;
            }
            else
            {
                _bodyTransform = null;
                _rigidbody = null;
                _isEnabled = false;
            }
        }

        public void TouchBegin()
        {
            if (_isEnabled && (_state == CharacterState.Idle || _state == CharacterState.Walk ))
            {
                _stateHolder.SetState(CharacterState.PrepareJump);
            }
        }

        public void TouchEnd(Vector2 worldPosition)
        {
            if (_isEnabled && _state == CharacterState.PrepareJump)
            {
                Vector2 jumpDirection =  (Vector2)_bodyTransform.position - worldPosition;
                float sqrDistance = jumpDirection.sqrMagnitude;

                if (sqrDistance <= _cancelJumpSqrDistance)
                { 
                    _stateHolder.SetState(CharacterState.Idle);
                }
                else if (CheckIsJumpDirectionGood(jumpDirection))
                {
                    jumpDirection.Normalize();

                    float distance = Mathf.Sqrt(sqrDistance);
                    float jumpForce = CalculateJumpForce(distance);

                    _rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
                    _stateHolder.SetState(CharacterState.FliesUp);
                }
                else
                {
                    _stateHolder.SetState(CharacterState.Idle);
                }

            }
        }

        private bool CheckIsJumpDirectionGood(Vector2 direction)
        {
            return direction.y > 0.0f;
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }

        private float CalculateJumpForce(float distance)
        {
            if (distance > _maxJumpForceDistance)
            {
                distance = _maxJumpForceDistance;
            }
            return _k * distance + _b;
        }

        #endregion

    }
}
