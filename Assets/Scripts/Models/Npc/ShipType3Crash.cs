using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class ShipType3Crash : PooledObject, IExecutable, IInitializable
    {
        #region Fields

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

        private Vector3[] _positions;
        private Quaternion[] _rotations;

        private float _breakAngle = 45.0f;
        private float _ballonDeactivateYPosition;

        private bool _isBowConnected;
        private bool _isSternConnected;
        private bool _isBallonEnabled;

        #endregion


        #region UnityMethods

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
            _isBallonEnabled = true;

            _ballonDeactivateYPosition = Services.Instance.SceneGeometry.GetVisibleArea().yMax + 
                BALLON_DEACTIVATE_OFFSET;

        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            PrepareToReturnToPool();
        }

        #endregion


        #region Methods

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
            if (!_isBallonEnabled)
            {
                _ballon.gameObject.SetActive(true);
                _isBallonEnabled = true;
            }
        }

        public override void PrepareToReturnToPool()
        {
            base.PrepareToReturnToPool();
            RestoreStartingState();
            Services.Instance.UpdateService.RemoveFromUpdate(this);
        }

        #endregion


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

            if (_isBallonEnabled)
            {
                if (_ballon.position.y > _ballonDeactivateYPosition)
                {
                    _ballon.gameObject.SetActive(false);
                    _isBallonEnabled = false;
                }
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            Services.Instance.UpdateService.AddToUpdate(this);
        }

        #endregion

    }
}
