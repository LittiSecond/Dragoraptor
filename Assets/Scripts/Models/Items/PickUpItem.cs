using System;
using UnityEngine;


namespace Dragoraptor
{
    public class PickUpItem : PooledObject, IInitializable
    {
        #region Fields

        [SerializeField] private float _activationDelay = 0.0f;

        private PickableResource[] _content;
        private float _startTime;

        #endregion


        #region UnityMethods

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

        #endregion


        #region Methods

        public void SetContent(PickableResource[] newContent)
        {
            _content = newContent;
        }

        #endregion


        #region IInitializable

        public virtual void Initialize()
        {
            _startTime = Time.time;
        }

        #endregion

    }
}
