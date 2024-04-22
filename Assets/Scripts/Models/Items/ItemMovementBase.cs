using UnityEngine;


namespace Dragoraptor
{
    public abstract class ItemMovementBase : IExecutable
    {

        protected Transform _itemRoot;


        public ItemMovementBase(Transform itemRoot)
        {
            _itemRoot = itemRoot;
        }


        public virtual void SetData(float[] data)
        {
            
        }

        public virtual void Start()
        {
            Services.Instance.UpdateService.AddToUpdate(this);
        }

        public virtual void Stop()
        {
            Services.Instance.UpdateService.RemoveFromUpdate(this);
        }

        #region IExecutable

        public virtual void Execute()
        {
            
        }

        #endregion
        
        
        
    }
}