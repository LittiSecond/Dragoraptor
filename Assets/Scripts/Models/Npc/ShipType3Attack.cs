using UnityEngine;
using System;


namespace Dragoraptor
{
    public class ShipType3Attack : IExecutable, IInitializable
    {
        #region Fields

        private string BULLET_ID = "StoneBall";
        private float MAX_X_DEVIATION = 0.3f;


        private readonly Transform _bulletStartPoint;

        private float _reloadTime;
        private float _timeCounter;
        private int _damag;

        private bool _isReady;

        #endregion

        #region ClassLifeCycles

        public ShipType3Attack(Transform bulletStartPoint, float reloadTime, int damag)
        {
            _bulletStartPoint = bulletStartPoint;
            _reloadTime = reloadTime;
            _damag = damag;
        }

        #endregion


        #region Methods

        private void Attack()
        {
            PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(BULLET_ID);
            if (obj != null)
            {
                FreeFallStone bullet = obj as FreeFallStone;
                if (bullet != null)
                {
                    bullet.transform.position = _bulletStartPoint.position;
                    bullet.Damag = _damag;
                    bullet.Kick();
                }
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isReady)
            {
                Vector3? targetPosition = Services.Instance.CharacterIntermediary.GetPlayerPosition();
                if (targetPosition.HasValue)
                {
                    float dx = _bulletStartPoint.position.x - targetPosition.Value.x; 

                    if ( dx > -MAX_X_DEVIATION && dx < MAX_X_DEVIATION)
                    {
                        Attack();
                        _isReady = false;
                    }
                }
            }
            else
            {
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= _reloadTime)
                {
                    _timeCounter = 0.0f;
                    _isReady = true;
                }
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            _timeCounter = 0.0f;
            _isReady = false;
        }

        #endregion

    }
}