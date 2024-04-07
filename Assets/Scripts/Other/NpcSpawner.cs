using System;
using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class NpcSpawner : IExecutable, INpcSpawner
    {

        #region Fields

        private readonly List<NpcBaseLogick> _npcList;
        private readonly List<string> _npcTypes;
        private NpcSpawnRule _spawnRule;
        private Action<NpcBaseLogick> _onDestroyListener;
        private NpcSpawnFixedChain _fixedChainLogic;
        private NpcSpawnCyclicChain _cyclicChainLogic;

        private bool _isEnabled;

        #endregion


        #region ClassLifeCycles

        public NpcSpawner(List<NpcBaseLogick> list, Action<NpcBaseLogick> onDestroyListener)
        {
            _npcList = list;
            _npcTypes = new List<string>();
            _onDestroyListener = onDestroyListener;
            _fixedChainLogic = new NpcSpawnFixedChain(this);
            _cyclicChainLogic = new NpcSpawnCyclicChain(this);
        }

        #endregion


        #region Methods

        public void SetSpawnRule(NpcSpawnRule rule)
        {
            StopSpawnLogick();

            _spawnRule = rule;

            NpcBaseLogick[] prefabs = _spawnRule.Prefabs;

            if (_npcTypes.Count > 0)
            {
                _npcTypes.Clear();
            }
            for (int i = 0; i < prefabs.Length; i++)
            {
                _npcTypes.Add(prefabs[i].Type);
            }

            _fixedChainLogic.SetSpawnData(_spawnRule.SpawnDatas);
            _cyclicChainLogic.SetSpawnData(_spawnRule.CyclicSpawnDatas, _spawnRule.CycleDuration);
        }

        public void RestartNpcSpawn()
        {
            StopSpawnLogick();
            StartSpawnLogick();
        }

        private void StopSpawnLogick()
        {
            _isEnabled = false;
            _fixedChainLogic.StopSpawnLogic();
            _cyclicChainLogic.StopSpawnLogic();
        }

        private void StartSpawnLogick()
        {
            _fixedChainLogic.StartSpawnLogic();
            _cyclicChainLogic.StartSpawnLogic();
            _isEnabled = true;
        }

        #endregion


        #region INpcSpawner

        public void SpawnNpc(SpawnData spawnData)
        {
            string npsType = _npcTypes[spawnData.PrefabIndex];
            PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(npsType);
            if (obj != null)
            {
                NpcBaseLogick npc = obj as NpcBaseLogick;
                if (npc != null)
                {
                    npc.transform.position = spawnData.SpawnPosition;
                    npc.OnDestroy += _onDestroyListener;
                    npc.SetAdditionalData(spawnData.Data);
                    npc.SetAdditionalDataArray(spawnData.FloatDatas);
                    npc.Initialize();

                    _npcList.Add(npc);
                }
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled)
            {
                _fixedChainLogic.Execute();
                _cyclicChainLogic.Execute();
            }
        }

        #endregion
    }
}
