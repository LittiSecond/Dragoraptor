﻿using System;
using UnityEngine;


namespace Dragoraptor
{
    sealed class Bird1Movement : IExecutable, IInitializable, ICleanable
    {
        #region Fields

        public event Action OnWayFinished;

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly INpcDirection _visualDirection;

        private Vector2[] _way; 

        private Vector2 _destination;

        private float _arrivalDistanceSqr = 0.01f;
        private float _speed = 1.0f;
        private int _nexWayPointIndex;

        private bool _haveWay;
        private bool _isCyclic;
        private bool _isEnabled;

        #endregion



        #region Methods

        public Bird1Movement(Transform transform, Rigidbody2D rigidbody, INpcDirection direction)
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

        private bool SelectNewDestination()
        {
            bool isSelected = false;
            if (_haveWay)
            {
                _nexWayPointIndex++;
                if (_nexWayPointIndex >= _way.Length)
                {
                    if (_isCyclic)
                    {
                        _nexWayPointIndex = 0;
                        _destination = _way[_nexWayPointIndex];
                        isSelected = true;
                    }
                }
                else
                {
                    _destination = _way[_nexWayPointIndex];
                    isSelected = true;
                }
            }
            return isSelected;
        }

        public void SetWay(NpcDataWay way)
        {
            _way = way.Way;
            _isCyclic = way.IsCyclic;
            _haveWay = _way != null;
            _nexWayPointIndex = 0;

        }

        public void StopMovementLogick()
        {
            _isEnabled = false;
        }

        private void StopMovement()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled)
            {
                Vector2 direction = _destination - (Vector2)_transform.position;
                if (direction.sqrMagnitude <= _arrivalDistanceSqr)
                {
                    if (SelectNewDestination())
                    {
                        SetFlightDirection(_destination);
                    }
                    else
                    {
                        StopMovement();
                        StopMovementLogick();
                        OnWayFinished?.Invoke();
                    }
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
                _isEnabled = true;
                _destination = _way[_nexWayPointIndex];
                SetFlightDirection(_destination);
            }
        }

        #endregion


        #region ICleanable

        public void Clear()
        {
            _isEnabled = false;
            _haveWay = false;
            _way = null;
        }

        #endregion

    }
}
