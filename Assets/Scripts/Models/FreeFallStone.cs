using UnityEngine;
using System.Collections;


namespace Dragoraptor
{
    public class FreeFallStone : PooledObject
    {
        #region Fields

        [SerializeField] private Fading _fadingLogick;
        [SerializeField] private int _damag;

        private bool _isDamagEnabled;
        private bool _isFadingEnabled;

        #endregion


        #region Properties

        public int Damag { set => _damag = value; }

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject other = collision.gameObject;
            
            if (_isDamagEnabled)
            {
                if (other.layer == (int)SceneLayer.Player)
                {
                    ITakeDamag damagReceiver = other.GetComponent<ITakeDamag>();
                    if (damagReceiver != null)
                    {
                        damagReceiver.TakeDamage(_damag);
                        _isDamagEnabled = false;
                    }
                }
            }

            if (_isFadingEnabled)
            {
                if (other.layer == (int)SceneLayer.Ground)
                {
                    _fadingLogick.StartFading();
                    _isFadingEnabled = false;
                    _isDamagEnabled = false;
                }
            }
        }

        #endregion


        #region Methods

        public void Kick()
        {
            _isDamagEnabled = true;
            _isFadingEnabled = true;
            _fadingLogick.OnFadingEnd += DestroyItself;
        }

        private void DestroyItself()
        {
            _fadingLogick.OnFadingEnd -= DestroyItself;
            ReturnToPool();
        }

        #endregion
    }
}