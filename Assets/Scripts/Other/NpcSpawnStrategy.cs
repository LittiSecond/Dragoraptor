using System;
using UnityEngine;


namespace Dragoraptor
{
    public abstract class NpcSpawnStrategy
    {
        #region Fields

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public abstract void SetSpawnData(SpawnData[] spawnDatas);

        public abstract void StartSpawnLogick();

        public abstract void StopSpawnLogick();

        public abstract void Execute();

        #endregion
    }
}