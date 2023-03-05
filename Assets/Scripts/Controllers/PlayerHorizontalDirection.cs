using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerHorizontalDirection : IBodyUser
    {
        #region Fields

        private PlayerBody _playerBody;
        private Transform _transform;

        private Direction _direction;

        #endregion


        #region Methods

        public void SetTouchPosition(Vector2 position)
        {
            Direction direction = (_transform.position.x > position.x) ? Direction.Rigth : Direction.Left;
            SetBodyDirection(direction);
        }

        public void SetDistination(Vector2 position)
        {
            Direction direction = (_transform.position.x < position.x)? Direction.Rigth : Direction.Left;
            SetBodyDirection(direction);
        }

        private void SetBodyDirection(Direction direction)
        {
            if (direction != _direction)
            {
                _direction = direction;
                _playerBody.SetDirection(_direction);
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
