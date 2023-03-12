using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcMoveToPosition : ICleanable
    {
        #region Fields

        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private float _speed;

        private ITimeRemaining _timer;

        #endregion


        #region ClassLifeCycles

        public NpcMoveToPosition(Rigidbody2D rb2d, Transform tr, float speed)
        {
            _rigidbody = rb2d;
            _transform = tr;
            _speed = speed;
        }

        #endregion


        #region Methods

        public void MoveToPosition(Vector2 destination)
        {
            if (_speed > 0.0f)
            {
                Vector2 position = _transform.position;
                Vector2 direction = destination - position;
                Vector2 newVelocity = direction.normalized * _speed;

                float duration = direction.magnitude / _speed;
                if (_timer != null)
                {
                    _timer.RemoveTimeRemaining();
                }
                _timer = new TimeRemaining(OnArrival, duration);
                _timer.AddTimeRemaining();

                _rigidbody.velocity = newVelocity;
            }
        }

        private void OnArrival()
        {
            _timer = null;
            _rigidbody.velocity = Vector2.zero;
        }

        #endregion


        #region ICleanable

        public void Clear()
        {
            if (_timer != null)
            {
                _timer.RemoveTimeRemaining();
                _timer = null;
            }
        }

        #endregion
    }
}
