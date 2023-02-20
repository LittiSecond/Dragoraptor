using UnityEngine;


namespace Dragoraptor
{
    public sealed class JumpController
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        private WalkController _walkController;
        private JumpPainter _jumpPainter;

        private float _maxJumpForce = 10.0f;
        private float _jumpForce;

        private bool _isEnabled;
        private bool _isJumpPreparation;
        private bool _isInFlight;

        #endregion


        #region ClassLifeCycles

        public JumpController(WalkController pw, JumpPainter jp)
        {
            _walkController = pw;
            _jumpPainter = jp;
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
                _isJumpPreparation = false;
                _isInFlight = false;
                _isEnabled = false;
            }
        }

        public void TouchBegin(Vector2 worldPosition)
        {
            if (_isEnabled && !_isInFlight)
            {
                _walkController.StopMovement();
                _walkController.JumpBegin();
                _isJumpPreparation = true;
            }
        }

        public void TouchEnd(Vector2 worldPosition)
        {
            if (_isEnabled)
            {
                if (_isJumpPreparation)
                {
                    Vector2 jumpDirection =  (Vector2)_transform.position - worldPosition;

                    if (CheckIsJumpDirectionGood(jumpDirection))
                    {
                        jumpDirection.Normalize();

                        _rigidbody.AddForce(jumpDirection * _jumpForce, ForceMode2D.Impulse);
                        _playerBody.OnGroundContact += OnGroundContact;
                        _isInFlight = true;
                    }
                }

                _isJumpPreparation = false;
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
            _isInFlight = false;
            _walkController.JumpEnd();
        }

        #endregion


    }
}
