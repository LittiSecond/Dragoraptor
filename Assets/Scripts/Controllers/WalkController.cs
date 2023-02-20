using UnityEngine;


namespace Dragoraptor
{
    public sealed class WalkController : IExecutable
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private readonly CharacterStateHolder _stateHolder;

        private Vector2 _velocity;

        private float _speed = 1.0f;
        private float _xDestination;

        private CharacterState _state;

        private bool _isEnabled;
        private bool _shouldMove;
        private bool _isDirectionRigth;

        #endregion


        #region ClassLifeCycles

        public WalkController(CharacterStateHolder csh)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;
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
                if (_isEnabled)
                {
                    StopMovement();
                }
                _playerBody = null;
                _transform = null;
                _rigidbody = null;
                _isEnabled = false;
            }
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
            _isDirectionRigth = _xDestination > x;

            float direction = _isDirectionRigth ? 1.0f : -1.0f;
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

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled && _shouldMove)
            {
                float x = _transform.position.x;
                if (_isDirectionRigth && (x >= _xDestination))
                {
                    StopMovement();
                    _stateHolder.SetState(CharacterState.Idle);
                }
                else if ( !_isDirectionRigth && (x <= _xDestination))
                {
                    StopMovement();
                    _stateHolder.SetState(CharacterState.Idle);
                }
            }
        }

        #endregion

    }
}
