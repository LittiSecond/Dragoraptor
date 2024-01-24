using UnityEngine;


namespace Dragoraptor
{
    public sealed class WalkController : IExecutable, IBodyUser
    {

        private PlayerBody _playerBody;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private readonly CharacterStateHolder _stateHolder;

        private Vector2 _velocity;

        private float _speed;
        private float _xDestination;

        private CharacterState _state;

        private bool _isEnabled;
        private bool _shouldMove;
        private bool _isDirectionRight;


        public WalkController(CharacterStateHolder csh, GamePlaySettings gps)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;
            _speed = gps.WalkSpeed;
        }


        public void SetDestination(float x)
        {
            if (_isEnabled && (_state == CharacterState.Idle || _state == CharacterState.Walk))
            {
                _xDestination = x;
                StartMovement();
                if (_state == CharacterState.Idle)
                {
                    _stateHolder.SetState(CharacterState.Walk);
                }
            }
        }

        private void StopMovement()
        {
            if (_isEnabled && _state == CharacterState.Walk)
            {
                _velocity = Vector2.zero;
                _rigidbody.velocity = Vector2.zero;
                _shouldMove = false;
            }
        }

        private void StartMovement()
        {
            float x = _transform.position.x;
            _isDirectionRight = _xDestination > x;

            float direction = _isDirectionRight ? 1.0f : -1.0f;
            _velocity = new Vector2(_speed * direction, 0);
            _rigidbody.velocity = _velocity;

            _shouldMove = true;
        }

        private void OnStateChanged(CharacterState newState)
        {
            if (newState != _state)
            {
                if (_state == CharacterState.Walk)
                {
                    StopMovement();
                }

                _state = newState;
            }
        }


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _playerBody = pb;
            _transform = _playerBody.transform;
            _rigidbody = _playerBody.GetRigidbody();
            _isEnabled = true;
        }

        public void ClearBody()
        {
            if (_isEnabled)
            {
                StopMovement();
            }
            _playerBody = null;
            _transform = null;
            _rigidbody = null;
            _isEnabled = false;
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled && _shouldMove)
            {
                float x = _transform.position.x;
                if (_isDirectionRight && (x >= _xDestination))
                {
                    StopMovement();
                    _stateHolder.SetState(CharacterState.Idle);
                }
                else if ( !_isDirectionRight && (x <= _xDestination))
                {
                    StopMovement();
                    _stateHolder.SetState(CharacterState.Idle);
                }
            }
        }

        #endregion

    }
}
