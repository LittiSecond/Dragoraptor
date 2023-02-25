using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpController : IBodyUser 
    {
        #region Fields

        private Transform _bodyTransform;
        private Rigidbody2D _rigidbody;

        private readonly CharacterStateHolder _stateHolder;
        private readonly JumpCalculator _jumpCalculator;

        private CharacterState _state;

        private bool _haveBody;

        #endregion


        #region ClassLifeCycles

        public JumpController(CharacterStateHolder csh, JumpCalculator jc)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;

            _jumpCalculator = jc;
        }

        #endregion


        #region Methods

        public void TouchBegin()
        {
            if (_haveBody && (_state == CharacterState.Idle || _state == CharacterState.Walk ))
            {
                _stateHolder.SetState(CharacterState.PrepareJump);
            }
        }

        public void TouchEnd(Vector2 worldPosition)
        {
            if (_haveBody && _state == CharacterState.PrepareJump)
            {
                Vector2 jumpDirection =  (Vector2)_bodyTransform.position - worldPosition;

                Vector2 impulse = _jumpCalculator.CalculateJampImpulse(jumpDirection);

                if (impulse != Vector2.zero)
                {
                    _rigidbody.AddForce(impulse, ForceMode2D.Impulse);
                    _stateHolder.SetState(CharacterState.FliesUp);
                }
                else
                {
                    _stateHolder.SetState(CharacterState.Idle);
                }

            }
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }

        #endregion


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _bodyTransform = pb.transform;
            _rigidbody = pb.GetRigidbody();
            _haveBody = true;
        }

        public void ClearBody()
        {
            _bodyTransform = null;
            _rigidbody = null;
            _haveBody = false;
        }

        #endregion
    }
}
