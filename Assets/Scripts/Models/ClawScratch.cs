using UnityEngine;

namespace Dragoraptor
{
    public class ClawScratch: PooledObject, IInitializable
    {

        [SerializeField] private SpriteRenderer _image;
        [SerializeField] private float _liveTime = 0.5f;
        
        private ITimeRemaining _timer;
        private bool _isTiming;
        
        
        #region IInitializable

        public void Initialize()
        {
            _timer ??= new TimeRemaining(DestroyItself, _liveTime);
            _timer.AddTimeRemaining();
            _isTiming = true;
            
            RotateToCharacter();
        }

        #endregion
        
        
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

        private void RotateToCharacter()
        {
            var pos = Services.Instance.CharacterIntermediary.GetPlayerPosition();
            if (pos.HasValue)
            {
                if (transform.position.x - pos.Value.x > 0.0f)
                {
                    _image.flipX = false;
                }
                else
                {
                    _image.flipX = true;
                }
                
            }
        }
        
        
    }
}