using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerMovement : IExecutable
    {
        #region Fields



        private PlayerBody _playerBody;
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        private Vector2 _velocity;

        private float _speed = 1.0f;
        private float _xDestination;


        private bool _isEnabled;
        private bool _shouldMove;
        private bool _isDirectionRigth;

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
            if (_isEnabled)
            {
                _xDestination = x;
                StartMovement();
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
                }
                else if ( !_isDirectionRigth && (x <= _xDestination))
                {
                    StopMovement();
                }
            }
        }

        private void StopMovement()
        {
            if (_isEnabled)
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

        #endregion


    }
}
