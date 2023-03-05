using System;
using UnityEngine;
using System.Collections.Generic;


namespace Dragoraptor
{
    public class NpcBaseLogick : PooledObject, IExecutable
    {
        #region Fields

        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected Collider2D _collider;
        [SerializeField] protected SpriteRenderer _mainSprite;

        public event Action<NpcBaseLogick> OnDestroy;

        private readonly List<IExecutable> _executeList = new List<IExecutable>();
        private readonly List<IInitializable> _initializeList = new List<IInitializable>();

        protected bool _isEnabled;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {

        }

        #endregion


        #region Methods

        public virtual void DestroyItSelf()
        {
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

        public virtual void SetAdditionalData(NpcData additionalData)
        {

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

    }
}
