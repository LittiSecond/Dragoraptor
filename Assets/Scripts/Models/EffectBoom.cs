using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class EffectBoom : PooledObject, IInitializable
    {
        #region Fields

        [SerializeField] private float _liveTime = 2.0f;

        private ITimeRemaining _timer;
        private bool _isTiming;

        #endregion


        #region Methods

        private void DestroyItself()
        {
            _isTiming = false;
            ReturnToPool();
        }

        public override void PrepareToReturnToPool()
        {
            base.PrepareToReturnToPool();
            if (_isTiming)
            {
                _timer.RemoveTimeRemaining();
                _isTiming = false;
            }
        }

        #endregion


        #region IInitializable

        public void Initialize()
        {
            if (_timer == null)
            {
                _timer = new TimeRemaining(DestroyItself, _liveTime);
            }
            _timer.AddTimeRemaining();
            _isTiming = true;
        }

        #endregion

    }
}
