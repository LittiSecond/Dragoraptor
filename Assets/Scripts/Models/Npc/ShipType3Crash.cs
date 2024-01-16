using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class ShipType3Crash : PooledObject, IExecutable, IInitializable
    {

        private const float FULL_TURN = 360.0f;
        private const float BALLON_DEACTIVATE_OFFSET = 1.0f;

        [SerializeField] private Transform[] _toSaveState;
        [SerializeField] private Transform _bow;
        [SerializeField] private Transform _stern;
        [SerializeField] private Transform _ballon;
        [SerializeField] private HingeJoint2D _bowJoint;
        [SerializeField] private HingeJoint2D _sternJoint;
        [SerializeField] private Rigidbody2D _bowRigedbody;
        [SerializeField] private Rigidbody2D _sternRigedbody;
        [SerializeField] private float _selfDestroyDelay = 8.0f;

        private Vector3[] _positions;
        private Quaternion[] _rotations;

        private float _breakAngle = 45.0f;
        private float _balloonDeactivateYPosition;
        private float _destroyTimeCounter;

        private bool _isBowConnected;
        private bool _isSternConnected;
        private bool _isBalloonEnabled;


        private void Awake()
        {
            _positions = new Vector3[_toSaveState.Length];
            _rotations = new Quaternion[_toSaveState.Length];

            for (int i = 0; i < _toSaveState.Length; i++)
            {
                _positions[i] = _toSaveState[i].localPosition;
                _rotations[i] = _toSaveState[i].localRotation;
            }
            _isBowConnected = true;
            _isSternConnected = true;
            _isBalloonEnabled = true;

            _balloonDeactivateYPosition = Services.Instance.SceneGeometry.GetVisibleArea().yMax + 
                BALLON_DEACTIVATE_OFFSET;

        }


        private void RestoreStartingState()
        {
            for (int i = 0; i < _toSaveState.Length; i++)
            {
                _toSaveState[i].localPosition = _positions[i];
                _toSaveState[i].localRotation = _rotations[i];
            }
            _bowJoint.enabled = true;
            _sternJoint.enabled = true;
            _isBowConnected = true;
            _isSternConnected = true;
            if (!_isBalloonEnabled)
            {
                _ballon.gameObject.SetActive(true);
                _isBalloonEnabled = true;
            }
        }

        public override void PrepareToReturnToPool()
        {
            base.PrepareToReturnToPool();
            RestoreStartingState();
            Services.Instance.UpdateService.RemoveFromUpdate(this);
        }

        private void DestroyItself()
        {
            PrepareToReturnToPool();
            ReturnToPool();
        }


        #region IExecutable

        public void Execute()
        {
            if (_isBowConnected)
            {
                float zAngle = _bow.transform.localRotation.eulerAngles.z;
                if (zAngle > _breakAngle && zAngle < FULL_TURN - _breakAngle)
                {
                    _bowJoint.enabled = false;
                    _isBowConnected = false;
                    Vector2 velosity = _bowRigedbody.velocity;
                    velosity.x = 0.0f;
                    _bowRigedbody.velocity = velosity;
                }
            }

            if (_isSternConnected)
            {
                float zAngle = _stern.localRotation.eulerAngles.z;
                if (zAngle > _breakAngle && zAngle < FULL_TURN - _breakAngle)
                {
                    _sternJoint.enabled = false;
                    _isSternConnected = false;
                    Vector2 velosity = _sternRigedbody.velocity;
                    velosity.x = 0.0f;
                    _sternRigedbody.velocity = velosity;
                }
            }

            if (_isBalloonEnabled)
            {
                if (_ballon.position.y > _balloonDeactivateYPosition)
                {
                    _ballon.gameObject.SetActive(false);
                    _isBalloonEnabled = false;
                }
            }

            _destroyTimeCounter += Time.deltaTime;
            if (_destroyTimeCounter >= _selfDestroyDelay)
            {
                DestroyItself();
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _destroyTimeCounter = 0.0f;
            Services.Instance.UpdateService.AddToUpdate(this);
        }

        #endregion

    }
}
