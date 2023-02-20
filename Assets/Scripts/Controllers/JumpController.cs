using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpController
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        private readonly CharacterStateHolder _stateHolder;

        private float _maxJumpForce = 10.0f;
        private float _jumpForce;

        private CharacterState _state;

        private bool _isEnabled;

        #endregion


        #region ClassLifeCycles

        public JumpController(CharacterStateHolder csh)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;
            _jumpForce = _maxJumpForce;
        }

        #endregion


        #region Methods

        public void SetBody(PlayerBody pb)
        {
            if (pb)
            {
                _playerBody = pb;
                _transform = _playerBody.transform;
                _rigidbody = _playerBody.GetRigidbody();
                _isEnabled = true;
            }
            else
            {
                _playerBody = null;
                _transform = null;
                _rigidbody = null;
                _isEnabled = false;
            }
        }

        public void TouchBegin(Vector2 worldPosition)
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
                Vector2 jumpDirection =  (Vector2)_transform.position - worldPosition;

                if (CheckIsJumpDirectionGood(jumpDirection))
                {
                    jumpDirection.Normalize();

                    _rigidbody.AddForce(jumpDirection * _jumpForce, ForceMode2D.Impulse);
                    _playerBody.OnGroundContact += OnGroundContact;
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

        private void OnGroundContact()
        {
            _playerBody.OnGroundContact -= OnGroundContact;
            _rigidbody.velocity = Vector2.zero;
            _stateHolder.SetState(CharacterState.Idle);
        }

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
        }

        #endregion

    }
}
