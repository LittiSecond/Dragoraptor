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
        [SerializeField] private int _maxHealth;

        public event Action<NpcBaseLogick> OnDestroy;

        private NpcHealth _health;
        private readonly List<IExecutable> _executeList = new List<IExecutable>();
        private readonly List<IInitializable> _initializeList = new List<IInitializable>();
        private readonly List<ICleanable> _clearList = new List<ICleanable>();

        protected bool _isEnabled;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            _health = new NpcHealth(_maxHealth);
            _health.OnHealthEnd += OnHealthEnd;
            _initializeList.Add(_health);
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
            for (int i = 0; i < _clearList.Count; i++)
            {
                _clearList[i].Clear();
            }

            _isEnabled = false;
            OnDestroy?.Invoke(this);
            ReturnToPool();
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

        private void OnHealthEnd()
        {
            DestroyItSelf();
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled)
            {
                for (int i = 0; i < _executeList.Count; i++)
                {
                    _executeList[i].Execute();
                }
            }
        }

        #endregion


        #region ITakeDamag

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        #endregion

    }
}
