using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PlayerHorizontalDirection : IBodyUser
    {

        public event Action<Direction> OnDirectionChanged;

        private PlayerBody _playerBody;
        private Transform _transform;

        private Direction _direction;


        public void SetTouchPosition(Vector2 position)
        {
            Direction direction = (_transform.position.x > position.x) ? Direction.Rigth : Direction.Left;
            SetBodyDirection(direction);
        }

        public void SetDestination(Vector2 position)
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
                OnDirectionChanged?.Invoke(_direction);
            }
        }


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
