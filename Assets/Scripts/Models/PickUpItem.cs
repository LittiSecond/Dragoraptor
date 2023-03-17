using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class PickUpItem :  MonoBehaviour //PooledObject
    {
        #region Fields

        [SerializeField] private PickableResource[] _content;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == (int)SceneLayer.Player)
            {
                Services.Instance.CharacterIntermediary.PickUp(_content);
            }
        }

        #endregion


        #region Methods

        #endregion

    }
}
