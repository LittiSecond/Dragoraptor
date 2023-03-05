using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class ShipType3Movement : IExecutable
    {
        #region Fields

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private float _horizontalSpeed = 0.5f;
        private float _verticalSpeed = 0.1f;
        private float _minY = -1.0f;
        private float _maxXError = 0.1f;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public ShipType3Movement(Transform transform, Rigidbody2D rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }

        #endregion


        #region Methods

        #endregion


        #region IExecutable

        public void Execute()
        {
            Vector3? playerPosition = Services.Instance.CharacterIntermediary.GetPlayerPosition();
            if (playerPosition.HasValue)
            {

            }
            else
            {

            }

        }

        #endregion

    }
}
