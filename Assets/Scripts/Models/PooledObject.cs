using UnityEngine;


namespace Dragoraptor
{
    public class PooledObject : MonoBehaviour
    {

        #region Fields

        [SerializeField] protected string _type = string.Empty;
        public ObjectPool ObjectPool { get; private set; }

        #endregion


        #region Properties

        public bool IsUsed { get; set; }

        public string Type
        {
            get { return _type; }
        }

        #endregion


        #region Methods

        public void SetObjectPool(ObjectPool op)
        {
            ObjectPool = op;
        }

        protected virtual void ReturnToPool()
        {
            if (ObjectPool != null)
            {
                ObjectPool.ReturnToPool(this);
            }
        }

        public virtual void PrepareToReturnToPool()
        {

        }

        #endregion
    }
}