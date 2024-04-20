using System;
using UnityEngine;
using System.Collections.Generic;


namespace Dragoraptor
{
    public class NpcBaseLogick : PooledObject, IExecutable, ITakeDamage, ILiveCycleHolder, IHealthEndHolder
    {

        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected Collider2D _collider;
        [SerializeField] protected SpriteRenderer _mainSprite;
        [SerializeField] private HpIndicator _hpIndicator;
        [SerializeField] private Transform _flyingDamagStartPoint;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _armor;
        [SerializeField] private int _scoreCost;
        [SerializeField] private string _dropItemID;
        [SerializeField] private PickableResource _dropContent;

        public event Action<NpcBaseLogick> OnDestroy;
        
        #region IHealthEndHolder

        public event Action OnHealthEnd;

        #endregion
        

        private NpcHealth _health;
        private NpcFlyingDamagCreator _flyingDamageCreator;
        private readonly List<IExecutable> _executeList = new List<IExecutable>();
        private readonly List<IInitializable> _initializeList = new List<IInitializable>();
        private readonly List<ICleanable> _clearList = new List<ICleanable>();

        private float _destroyTimeCounter;

        protected bool _isEnabled;
        private bool _isDestroyTimer;


        protected virtual void Awake()
        {
            _health = new NpcHealth(_maxHealth, _armor);
            _health.OnHealthEnd += () =>
            {
                OnHealthEnd?.Invoke();
                OnHealthEnded();
            };
            
            
            _initializeList.Add(_health);
            if (_flyingDamagStartPoint)
            {
                _flyingDamageCreator = new NpcFlyingDamagCreator(_flyingDamagStartPoint);
                _health.SetDamageObserver(_flyingDamageCreator);
            }
        }

        protected virtual void Start()
        {
            if (_hpIndicator)
            {
                _hpIndicator.SetIHealth(_health);
            }
        }


        public virtual void DestroyItSelf()
        {
            PrepareToReturnToPool();
            ReturnToPool();
        }

        protected void DestroyItselfDelay(float delay)
        {
            _destroyTimeCounter = delay;
            _isDestroyTimer = true;
        }

        public override void PrepareToReturnToPool()
        {
            base.PrepareToReturnToPool();
            for (int i = 0; i < _clearList.Count; i++)
            {
                _clearList[i].Clear();
            }

            _isEnabled = false;
            _isDestroyTimer = false;
            OnDestroy?.Invoke(this);
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < _initializeList.Count; i++)
            {
                _initializeList[i].Initialize();
            }
            _isEnabled = true;
        }


        #region ILiveCycleHolder
        
        public void AddExecutable(IExecutable executable)
        {
            _executeList.Add(executable);
        }

        public void AddInitializable(IInitializable initializable)
        {
            _initializeList.Add(initializable);
        }

        public void AddCleanable(ICleanable cleanable)
        {
            _clearList.Add(cleanable);
        }
        
        #endregion
        

        public virtual void SetAdditionalData(NpcData additionalData)
        {

        }

        public virtual void SetAdditionalDataArray(float[] datas)
        {

        }

        protected virtual void OnHealthEnded()
        {
            SendScoreReward();
            DropItem();
            DestroyItSelf();
        }

        protected virtual void DropItem()
        {
            if (CheckShouldDrop())
            {
                PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(_dropItemID);
                if (obj != null)
                {
                    PickUpItem item = obj as PickUpItem;
                    if (item != null)
                    {
                        item.transform.position = transform.position;
                        PickableResource[] content = new PickableResource[]
                        {
                            _dropContent
                        };
                        item.SetContent(content);
                        item.Initialize();
                    }
                }
            }
        }

        private bool CheckShouldDrop()
        {
            bool shouldDrop = false;

            if (!string.IsNullOrEmpty(_dropItemID))
            {
                if (_dropContent.Type != ResourceType.None)
                {
                    shouldDrop = true;
                }
            }

            return shouldDrop;
        }

        protected void SendScoreReward()
        {
            Services.Instance.CharacterIntermediary.AddScore(_scoreCost);
        }


        #region IExecutable

        public virtual void Execute()
        {
            if (_isEnabled)
            {
                for (int i = 0; i < _executeList.Count; i++)
                {
                    _executeList[i].Execute();
                }

                if (_isDestroyTimer)
                {
                    _destroyTimeCounter -= Time.deltaTime;
                    if (_destroyTimeCounter <= 0.0f)
                    {
                        _isDestroyTimer = false;
                        DestroyItSelf();
                    }
                }
            }
        }

        #endregion


        #region ITakeDamag

        public virtual void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        #endregion

    }
}
