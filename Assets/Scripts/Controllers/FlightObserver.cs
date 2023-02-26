using UnityEngine;


namespace Dragoraptor
{
    public sealed class FlightObserver : IExecutable, IBodyUser
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _bodyTransform;
        private Rigidbody2D _rigidbody;

        private readonly CharacterStateHolder _stateHolder;

        private float _previousY;

        private CharacterState _state;

        private bool _haveBody;
        private bool _isEnabled;
        private bool _isFirstFrame;

        #endregion


        #region ClassLifeCycles

        public FlightObserver(CharacterStateHolder csh)
        {
            _stateHolder = csh;
            _stateHolder.OnStateChanged += OnStateChanged;
        }

        #endregion


        #region Methods

        private void OnStateChanged(CharacterState newState)
        {
            _state = newState;
            _isEnabled = _haveBody && (_state == CharacterState.FliesUp || _state == CharacterState.FliesDown);

            if (_state == CharacterState.FliesUp)
            {
                _playerBody.OnGroundContact += OnGroundContact;
                _isFirstFrame = true;
            }
        }

        private void OnGroundContact()
        {
            _playerBody.OnGroundContact -= OnGroundContact;
            _rigidbody.velocity = Vector2.zero;
            _stateHolder.SetState(CharacterState.Idle);
        }

        #endregion


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _playerBody = pb;
            _bodyTransform = _playerBody.transform;
            _rigidbody = _playerBody.GetRigidbody();
            _haveBody = true;
        }

        public void ClearBody()
        {
            _playerBody = null;
            _bodyTransform = null;
            _rigidbody = null;
            _haveBody = false;
            _isEnabled = false;
            _isFirstFrame = false;
        }

        #endregion


        #region IExecutable

        public void Execute()
        { 
            if (_isEnabled)
            {
                float currentY = _bodyTransform.position.y;
                if (_state == CharacterState.FliesUp)
                {
                    if (_isFirstFrame)
                    {
                        _isFirstFrame = false;
                    }
                    else if (currentY < _previousY)
                    {
                        _stateHolder.SetState(CharacterState.FliesDown);
                    }
                }
                _previousY = currentY;
            }
        }

        #endregion
    }
}
