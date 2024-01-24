using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcFlyingDamagCreator : IDamageObserver
    {

        private const string TYPE = "FlyingDamag";

        private readonly Transform _startPoint;


        public NpcFlyingDamagCreator(Transform startPoint)
        {
            _startPoint = startPoint;
        }


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
