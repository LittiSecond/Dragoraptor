using System;
using UnityEngine;


namespace Dragoraptor
{
    public class PickUpItem : PooledObject, IInitializable
    {

        [SerializeField] private float _activationDelay = 0.0f;
        [SerializeField] private ItemMovementType _moveType;
        [SerializeField] private float[] _movementData;

        private ItemMovementBase _movementLogic;
        private PickableResource[] _content;
        private float _startTime;

        private bool _isInitialised;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Time.time - _startTime >= _activationDelay)
            {
                if (collision.gameObject.layer == (int)SceneLayer.Player)
                {
                    if (Services.Instance.CharacterIntermediary.PickUp(_content))
                    {
                        ReturnToPool();
                    }
                }
            }
        }


        public void SetContent(PickableResource[] newContent)
        {
            _content = newContent;
        }

        
        #region IInitializable

        public virtual void Initialize()
        {
            _startTime = Time.time;

            if (!_isInitialised)
            {
                _isInitialised = true;

                switch (_moveType)
                {
                    case ItemMovementType.None:
                        break;
                    case ItemMovementType.NearStartWave:
                        _movementLogic = new ItemMovementNearStartWave(transform);
                        _movementLogic.SetData(_movementData);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            _movementLogic?.Start();
        }

        #endregion


        public override void PrepareToReturnToPool()
        {
            base.PrepareToReturnToPool();
            _movementLogic?.Stop();
        }
    }
}
