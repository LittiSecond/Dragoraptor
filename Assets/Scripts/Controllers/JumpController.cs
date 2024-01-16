using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpController : IBodyUser 
    {

        private Transform _bodyTransform;
        private Rigidbody2D _rigidbody;

        private readonly CharacterStateHolder _stateHolder;
        private readonly JumpCalculator _jumpCalculator;
        private readonly IResouceStore _energyStore;

        private CharacterState _state;

        private bool _haveBody;


        public JumpController(CharacterStateHolder csh, JumpCalculator jc, IResouceStore energyStore)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;

            _jumpCalculator = jc;
            _energyStore = energyStore;
        }


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

                Vector2 impulse = _jumpCalculator.CalculateJumpImpulse(jumpDirection);

                if (impulse != Vector2.zero)
                {
                    int jumpCost = (int)_jumpCalculator.CalculateJumpCost();
                    if (_energyStore.SpendResource(jumpCost))
                    {
                        _rigidbody.AddForce(impulse, ForceMode2D.Impulse);
                        _stateHolder.SetState(CharacterState.FliesUp);
                    }
                    else
                    {
                        _stateHolder.SetState(CharacterState.Idle);
                    }
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
