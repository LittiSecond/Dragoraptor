using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class ShipType3Movement : IExecutable, ICleanable
    {
        
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;

        private Vector3 _velocity;

        private float _horizontalSpeed = 0.5f;
        private float _verticalSpeed = 0.1f;
        private float _minY = -1.0f;
        private float _maxXError = 0.1f;


        public ShipType3Movement(Transform transform, Rigidbody2D rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }


        private float CalculateVerticalSpeed()
        {
            return (_transform.position.y > _minY) ? -_verticalSpeed : 0.0f; 
        }

        private float CalculateHorizontalSpeed(float targetX)
        {
            float speed = 0.0f;
            float x = _transform.position.x;

            float dx = targetX - x;

            if (dx > _maxXError)
            {
                speed = _horizontalSpeed;
            }
            else if (dx < -_maxXError)
            {
                speed = -_horizontalSpeed;
            }

            return speed;
        }

        
        #region IExecutable

        public void Execute()
        {
            Vector3 newVelocity = Vector3.zero;
            Vector3? playerPosition = Services.Instance.CharacterIntermediary.GetPlayerPosition();
            if (playerPosition.HasValue)
            {
                newVelocity.x = CalculateHorizontalSpeed(playerPosition.Value.x);
                newVelocity.y = CalculateVerticalSpeed();
            }

            if (newVelocity != _velocity)
            {
                _rigidbody.velocity = newVelocity;
                _velocity = newVelocity;
            }

        }

        #endregion


        #region ICleanable

        public void Clear()
        {
            _rigidbody.velocity = Vector3.zero;
            _velocity = Vector3.zero;
        }

        #endregion
    }
}
