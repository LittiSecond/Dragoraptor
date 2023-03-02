using UnityEngine;


namespace Dragoraptor
{
    public class NpcBaseLogick : PooledObject, IExecutable
    {



        #region Methods

        public virtual void DestroyItSelf()
        {
            ReturnToPool();
        }

        #endregion


        #region IExecutable

        public void Execute()
        {

        }

        #endregion

    }
}
