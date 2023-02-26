using UnityEngine;


namespace Dragoraptor
{
    public sealed class HorizontalDirection : IBodyUser
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;

        bool _isDirectionRight;

        #endregion


        #region Methods

        public void SetTouchPosition(Vector2 position)
        {
            bool isRight = _transform.position.x > position.x;
            SetBodyDirection(isRight);
        }

        public void SetDistination(Vector2 position)
        {
            bool isRight = _transform.position.x < position.x;
            SetBodyDirection(isRight);
        }

        private void SetBodyDirection(bool isRight)
        {
            if (isRight != _isDirectionRight)
            {
                _isDirectionRight = isRight;
                _playerBody.SetDirectionIsRight(_isDirectionRight);
            }
        }

        #endregion


        #region IBodyUser

        public void SetBody(PlayerBody pb)
        {
            _playerBody = pb;
            _transform = _playerBody.transform;
        }

        public void ClearBody()
        {
            _playerBody = null;
            _transform = null;
        }

        #endregion
    }
}
