using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class ItemWithMovingWave : PickUpItem, IExecutable
    {



        #region Fields

        //private const float SPEED_REDUCTION = 0.5f;
        //private const float Y_SPEED_CHANGE_THRESHOLD = 0.6f;
        //private const float Y_DIRECTION_DOWN = -1.0f;
        //private const float Y_DIRECTION_UP = 1.0f;

        //[SerializeField] private Rigidbody2D _rigidbody;

        //[SerializeField] private float _maxHorizontalSpeed = 1.0f;
        //[SerializeField] private float _verticalMaxSpeed = 1.0f;
        //[SerializeField] private float _amplitude = 1.0f;
        //[SerializeField] private float _destractionXPosition = 4.5f;

        //private float _directionY = 1.0f;
        //private float _startY;
        //private float _verticalSpeed;
        //private float _horizontalSpeed;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        private void CalculateHorizontalSpeed()
        {
            float x = transform.position.x;
            if (x < 0)
            {
                //_horizontalSpeed = _maxHorizontalSpeed;
            }
            else
            {
                //_horizontalSpeed = -_maxHorizontalSpeed;
            }
        }

        #endregion


        #region IInitializable

        public override void Initialize()
        {
            base.Initialize();
            //_startY = transform.position.y;
            //_directionY = Y_DIRECTION_DOWN;
            CalculateHorizontalSpeed();
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            
        }

        #endregion
    }
}