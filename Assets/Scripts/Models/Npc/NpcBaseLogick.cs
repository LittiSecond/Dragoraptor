using System;
using UnityEngine;
using System.Collections.Generic;


namespace Dragoraptor
{
    public class NpcBaseLogick : PooledObject, IExecutable, ITakeDamag
    {
        #region Fields

        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected Collider2D _collider;
        [SerializeField] protected SpriteRenderer _mainSprite;
        [SerializeField] private HpIndicator _hpIndicator;
        [SerializeField] private Transform _flyingDamagStartPoint;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _scoreCost;
        [SerializeField] private string _dropItemID;
        [SerializeField] private PickableResource _dropContent;

        public event Action<NpcBaseLogick> OnDestroy;

        private NpcHealth _health;
        private NpcFlyingDamagCreator _flyingDamagCreator;
        private readonly List<IExecutable> _executeList = new List<IExecutable>();
        private readonly List<IInitializable> _initializeList = new List<IInitializable>();
        private readonly List<ICleanable> _clearList = new List<ICleanable>();

        private float _destroyTimeCounter;

        protected bool _isEnabled;
        private bool _isDestroyTimer;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            _health = new NpcHealth(_maxHealth);
            _health.OnHealthEnd += OnHealthEnd;
            _initializeList.Add(_health);
            if (_flyingDamagStartPoint)
            {
                _flyingDamagCreator = new NpcFlyingDamagCreator(_flyingDamagStartPoint);
            }
        }

        protected virtual void Start()
        {
            if (_hpIndicator)
            {
                _hpIndicator.SetIHealth(_health);
            }
        }

        #endregion


        #region Methods

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

        protected void AddExecutable(IExecutable executable)
        {
            _executeList.Add(executable);
        }

        protected void AddInitializable(IInitializable initializable)
        {
            _initializeList.Add(initializable);
        }

        protected void AddCleanable(ICleanable cleanable)
        {
            _clearList.Add(cleanable);
        }

        public virtual void SetAdditionalData(NpcData additionalData)
        {

        }

        public virtual void SetAdditionalDataArray(float[] datas)
        {

        }

        protected virtual void OnHealthEnd()
        {
            SendScoreRevard();
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

        protected void SendScoreRevard()
        {
            Services.Instance.CharacterIntermediary.AddScore(_scoreCost);
        }


        #endregion


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
            _flyingDamagCreator?.OnDamaged(amount);
            _health.TakeDamage(amount);
        }

        #endregion

    }
}
