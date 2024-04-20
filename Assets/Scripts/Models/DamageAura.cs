using UnityEngine;


namespace Dragoraptor
{
    public class DamageAura : MonoBehaviour, IInitializable, ICleanable//, IExecutable
    {

        private const string HIT_VISUAL_EFFECT = "EffectBoom";
        
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _effectSprite;
        [SerializeField] private int _damage;
//        [SerializeField] private Transform _rootToRotate;

        
        private ILiveCycleHolder _liveCycleHolder;

        private bool _isEnabled;
        

        private void Awake()
        {
            _liveCycleHolder = GetComponentInParent<ILiveCycleHolder>();
            _liveCycleHolder.AddInitializable(this);
            _liveCycleHolder.AddCleanable(this);
        }


        #region IInitializable

        public void Initialize()
        {
            _collider.enabled = true;
            _effectSprite.enabled = true;
            _isEnabled = true;
        }

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject other = collision.gameObject;
            
            if (other.layer == (int)SceneLayer.Player)
            {
                ITakeDamage damagReceiver = other.GetComponent<ITakeDamage>();
                if (damagReceiver != null)
                {
                    damagReceiver.TakeDamage(_damage);
                    CreateVisualHitEffect();
                }
                
                Deactivate();
            }
        }

        private void CreateVisualHitEffect()
        {
            PooledObject effect = Services.Instance.ObjectPool.GetObjectOfType(HIT_VISUAL_EFFECT);
            if (effect)
            {
                Vector3 position = transform.position;
                //position.y += _verticalOffsetVisualEffect;
                effect.transform.position = position;
                IInitializable initializable = effect as IInitializable;
                initializable?.Initialize();
            }
        }

        private void Deactivate()
        {
            _collider.enabled = false;
            _effectSprite.enabled = false;
            _isEnabled = false;
        }
        
        // #region IExecutable
        //
        // public void Execute()
        // {
        //     if (_isEnabled)
        //     {
        //         
        //     }
        // }
        //
        // #endregion

        #region ICleanable

        public void Clear()
        {
            Deactivate();
        }

        #endregion
        
        
    }
}