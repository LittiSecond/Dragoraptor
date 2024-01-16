using UnityEngine;
using System;


namespace Dragoraptor
{
    public class ShipType3Attack : IExecutable, IInitializable
    {

        private string BULLET_ID = "StoneBall";
        private float MAX_X_DEVIATION = 0.3f;

        private readonly Transform _bulletStartPoint;

        private float _reloadTime;
        private float _timeCounter;
        private int _damage;

        private bool _isReady;


        public ShipType3Attack(Transform bulletStartPoint, float reloadTime, int damage)
        {
            _bulletStartPoint = bulletStartPoint;
            _reloadTime = reloadTime;
            _damage = damage;
        }


        private void Attack()
        {
            PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(BULLET_ID);
            if (obj != null)
            {
                FreeFallStone bullet = obj as FreeFallStone;
                if (bullet != null)
                {
                    bullet.transform.position = _bulletStartPoint.position;
                    bullet.Damage = _damage;
                    bullet.Kick();
                }
            }
        }


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