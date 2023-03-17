using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PickUpItem : PooledObject
    {
        #region Fields

        private PickableResource[] _content;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == (int)SceneLayer.Player)
            {
                if ( Services.Instance.CharacterIntermediary.PickUp(_content))
                {
                    ReturnToPool();
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

    }
}
