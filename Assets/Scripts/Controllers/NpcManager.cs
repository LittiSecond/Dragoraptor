using System;
using System.Collections.Generic;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcManager : IExecutable
    {
        #region Fields

        private readonly List<NpcBaseLogick> _npcOnField;
        private readonly NpcSpawner _npcSpawner;

        private bool _isSpawnEnabled;
        private bool _isNpcLogickEnabled;
        private bool _haveSpawnRule;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public NpcManager()
        {
            _npcOnField = new List<NpcBaseLogick>();
            _npcSpawner = new NpcSpawner(_npcOnField);
        }

        #endregion



        #region Methods

        public void PrepareNpcSpawn()
        {
            if (_npcOnField.Count > 0)
            {
                ClearNpc();
            }
            if (_isSpawnEnabled)
            {
                StopNpcSpawn();
            }

            LevelDescriptor descriptor = Services.Instance.GameProgress.GetCurrentLevel();
            NpcSpawnRule spawnRule = descriptor.SpawnRule;

            if (spawnRule != null)
            {
                _haveSpawnRule = true;
                _npcSpawner.SetSpawnRule(spawnRule);
                NpcBaseLogick[] prefabs = spawnRule.Prefabs;
                PrepareObjectPool(prefabs);
            }
            else
            {
                _haveSpawnRule = false;
            }

        }

        public void RestartNpcSpawn()
        {
            if (_haveSpawnRule)
            {
                _npcSpawner.RestartNpcSpawn();
                _isSpawnEnabled = true;
            }
        }

        public void StopNpcSpawn()
        {
            _isSpawnEnabled = false;
        }

        public void ClearNpc()
        {
            for (int i = _npcOnField.Count - 1; i >= 0; i--)
            {
                _npcOnField[i].DestroyItSelf();
            }
            _npcOnField.Clear();
        }

        private void PrepareObjectPool(NpcBaseLogick[] prefabs)
        {
            PooledObject[] pooledObjects = new PooledObject[prefabs.Length];
            for (int i = 0; i < prefabs.Length; i++)
            {
                pooledObjects[i] = prefabs[i] as PooledObject;
            }
            Services.Instance.ObjectPool.PrepareObjects(pooledObjects);
        }

        private void ExecuteNpcLogick()
        {
            for (int i = 0; i < _npcOnField.Count; i++)
            {
                _npcOnField[i].Execute();
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isNpcLogickEnabled)
            {
                ExecuteNpcLogick();
            }

            if (_isSpawnEnabled)
            {
                _npcSpawner.Execute();
            }
        }

        #endregion

    }
}
