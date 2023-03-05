using UnityEngine;


namespace Dragoraptor
{
    sealed class Bird1Movement : IExecutable, IInitializable
    {
        #region Fields

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly NpcBaseDirection _visualDirection;
        private Rect _area;

        private Vector2[] _way = new Vector2[]
            { new Vector2(-1.0f, -1.0f), new Vector2(1.0f, -1.0f),  new Vector2(2.0f, 0.0f),
              new Vector2(1.0f, 1.0f),   new Vector2(-1.0f, 1.0f),  new Vector2(-2.0f, 0.0f)  };

        private Vector2 _destination;

        private float _arrivalDistanceSqr = 0.01f;
        private float _speed = 1.0f;
        private int _nexWayPointIndex;

        #endregion



        #region Methods

        public Bird1Movement(Transform transform, Rigidbody2D rigidbody, NpcBaseDirection direction)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _area = Services.Instance.SceneGeometry.GetVisibleArea();
            _visualDirection = direction;
        }

        private void SetFlightDirection(Vector2 destination)
        {
            Vector2 position = _transform.position;
            Vector2 direction = destination - position;
            direction.Normalize();
            Vector2 newVelocity = direction * _speed;
            _rigidbody.velocity = newVelocity;
            _visualDirection.IsDirectionToLeft = (direction.x < 0.0f);
        }

        private void SelectNewDestination()
        {
            _nexWayPointIndex++;
            if (_nexWayPointIndex >= _way.Length)
            {
                _nexWayPointIndex = 0;
            }
            _destination = _way[_nexWayPointIndex];
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            Vector2 direction = _destination - (Vector2)_transform.position;
            if (direction.sqrMagnitude <= _arrivalDistanceSqr)
            {
                SelectNewDestination();
                SetFlightDirection(_destination);
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _nexWayPointIndex = 0;
            _destination = _way[_nexWayPointIndex];
            SetFlightDirection(_destination);
        }

        #endregion

    }
}
