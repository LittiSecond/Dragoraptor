using System;
using UnityEngine;


namespace Dragoraptor
{
    public class PickUpItem : PooledObject, IInitializable
    {

        [SerializeField] private float _activationDelay = 0.0f;

        private PickableResource[] _content;
        private float _startTime;


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
        }

        #endregion

    }
}
