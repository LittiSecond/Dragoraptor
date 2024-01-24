using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class EffectBoom : PooledObject, IInitializable
    {
        private const int ADDITIONAL_BITS = 2;

        [SerializeField] private float _liveTime = 2.0f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;

        private ITimeRemaining _timer;
        private bool _isTiming;


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

        private void SelectSprite()
        {
            int rndValue = UnityEngine.Random.Range(0, _sprites.Length << ADDITIONAL_BITS);
            _spriteRenderer.flipX = (rndValue & 1) == 1;
            rndValue >>= 1;
            _spriteRenderer.flipY = (rndValue & 1) == 1;
            rndValue >>= 1;
            _spriteRenderer.sprite = _sprites[rndValue];
        }


        #region IInitializable

        public void Initialize()
        {
            if (_timer == null)
            {
                _timer = new TimeRemaining(DestroyItself, _liveTime);
            }
            _timer.AddTimeRemaining();
            _isTiming = true;
            SelectSprite();
        }

        #endregion

    }
}
