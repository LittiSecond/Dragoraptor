﻿using UnityEngine;
using System; 


namespace Dragoraptor
{
    public sealed class PlayerBody : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Rigidbody2D _rigedbody;
        [SerializeField] private LineRenderer _trajectoryRenderer;
        [SerializeField] private LineRenderer _powerRenderer;

        public event Action OnGroundContact;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (OnGroundContact != null)
            {
                if (collision.gameObject.layer == (int)SceneLayer.Ground)
                {
                    OnGroundContact();
                }
            }
        }

        #endregion


        #region Methods

        public Rigidbody2D GetRigidbody()
        {
            return _rigedbody;
        }

        public (LineRenderer, LineRenderer) GetLineRenderers()
        {
            return (_trajectoryRenderer, _powerRenderer);
        }

        #endregion

    }
}