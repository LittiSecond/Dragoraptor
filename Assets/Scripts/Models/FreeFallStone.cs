using UnityEngine;
using System.Collections;


namespace Dragoraptor
{
    public class FreeFallStone : PooledObject
    {
        #region Fields

        private const string HIT_VISUAL_EFFECT = "EffectBoom";

        [SerializeField] private Fading _fadingLogick;
        [SerializeField] private float _verticalOffsetVisualEffect = -0.2f;
        [SerializeField] private int _damag;

        private bool _isDamagEnabled;
        private bool _isFadingEnabled;

        #endregion


        #region Properties

        public int Damag { set => _damag = value; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _fadingLogick.OnFadingEnd += DestroyItself;
        }

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
                        CreateVisualHitEffect();
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
        }

        private void DestroyItself()
        {
            ReturnToPool();
        }

        private void CreateVisualHitEffect()
        {
            PooledObject effect = Services.Instance.ObjectPool.GetObjectOfType(HIT_VISUAL_EFFECT);
            if (effect)
            {
                Vector3 position = transform.position;
                position.y += _verticalOffsetVisualEffect;
                effect.transform.position = position;
                IInitializable initializable = effect as IInitializable;
                initializable?.Initialize();
            }
        }

        #endregion
    }
}