using UnityEngine;


namespace Dragoraptor
{
    sealed class Bird1Movement : IExecutable, IInitializable, ICleanable
    {
        #region Fields

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly NpcBaseDirection _visualDirection;

        private Vector2[] _way; 

        private Vector2 _destination;

        private float _arrivalDistanceSqr = 0.01f;
        private float _speed = 1.0f;
        private int _nexWayPointIndex;

        private bool _haveWay;

        #endregion



        #region Methods

        public Bird1Movement(Transform transform, Rigidbody2D rigidbody, NpcBaseDirection direction)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _visualDirection = direction;
        }

        private void SetFlightDirection(Vector2 destination)
        {
            if (_haveWay)
            {
                Vector2 position = _transform.position;
                Vector2 direction = destination - position;
                direction.Normalize();
                Vector2 newVelocity = direction * _speed;
                _rigidbody.velocity = newVelocity;
                _visualDirection.IsDirectionToLeft = (direction.x < 0.0f);
            }
        }

        private void SelectNewDestination()
        {
            if (_haveWay)
            {
                _nexWayPointIndex++;
                if (_nexWayPointIndex >= _way.Length)
                {
                    _nexWayPointIndex = 0;
                }
                _destination = _way[_nexWayPointIndex];
            }
        }

        public void SetWay(Vector2[] way)
        {
            _way = way;
            _haveWay = _way != null;
            _nexWayPointIndex = 0;
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_haveWay)
            {
                Vector2 direction = _destination - (Vector2)_transform.position;
                if (direction.sqrMagnitude <= _arrivalDistanceSqr)
                {
                    SelectNewDestination();
                    SetFlightDirection(_destination);
                }
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _nexWayPointIndex = 0;
            if (_haveWay)
            {
                _destination = _way[_nexWayPointIndex];
                SetFlightDirection(_destination);
            }
        }

        #endregion


        #region ICleanable

        public void Clear()
        {
            _haveWay = false;
            _way = null;
        }

        #endregion

    }
}
