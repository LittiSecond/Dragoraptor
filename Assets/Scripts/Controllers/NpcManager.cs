using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class NpcManager : IExecutable
    {

        private readonly List<NpcBaseLogick> _npcOnField;
        private readonly NpcSpawner _npcSpawner;

        private bool _isSpawnEnabled;
        private bool _isNpcLogicEnabled;
        private bool _haveSpawnRule;


        public NpcManager()
        {
            _npcOnField = new List<NpcBaseLogick>();
            _npcSpawner = new NpcSpawner(_npcOnField, OnDestroyNpc);
            _isNpcLogicEnabled = true;
        }


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
                _npcOnField[i].OnDestroy -= OnDestroyNpc;
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

        private void ExecuteNpcLogic()
        {
            for (int i = 0; i < _npcOnField.Count; i++)
            {
                _npcOnField[i].Execute();
            }
        }

        private void OnDestroyNpc(NpcBaseLogick npc)
        {
            _npcOnField.Remove(npc);
            npc.OnDestroy -= OnDestroyNpc;
        }


        #region IExecutable

        public void Execute()
        {
            if (_isNpcLogicEnabled)
            {
                ExecuteNpcLogic();
            }

            if (_isSpawnEnabled)
            {
                _npcSpawner.Execute();
            }
        }

        #endregion

    }
}
