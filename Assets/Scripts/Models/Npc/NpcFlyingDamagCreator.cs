using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcFlyingDamagCreator : IDamageObserver
    {
        #region Fields

        private const string TYPE = "FlyingDamag";

        private readonly Transform _startPoint;

        #endregion


        #region ClassLifeCycles

        public NpcFlyingDamagCreator(Transform startPoint)
        {
            _startPoint = startPoint;
        }

        #endregion


        #region IDamageObserver

        public void OnDamaged(int amount)
        {
            PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(TYPE);
            if (obj)
            {
                FlyinngText text = obj as FlyinngText;
                text.transform.position = _startPoint.position;
                text.StartFlying((-amount).ToString());
            }
        }

        #endregion

    }
}
