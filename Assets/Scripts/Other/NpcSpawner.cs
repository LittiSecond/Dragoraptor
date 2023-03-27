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
        private NpcSpawnFixedChain _fixedChainLogick;
        private NpcSpawnCyclikChain _cyclikChainLogick;

        private bool _isEnabled;

        #endregion


        #region ClassLifeCycles

        public NpcSpawner(List<NpcBaseLogick> list, Action<NpcBaseLogick> onDestroyListener)
        {
            _npcList = list;
            _npcTypes = new List<string>();
            _onDestroyListener = onDestroyListener;
            _fixedChainLogick = new NpcSpawnFixedChain(this);
            _cyclikChainLogick = new NpcSpawnCyclikChain(this);
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

            _fixedChainLogick.SetSpawnData(_spawnRule.SpawnDatas);
            _cyclikChainLogick.SetSpawnData(_spawnRule.CyclicSpawnDatas, _spawnRule.CycleDuration);
        }

        public void RestartNpcSpawn()
        {
            StopSpawnLogick();
            StartSpawnLogick();
        }

        private void StopSpawnLogick()
        {
            _isEnabled = false;
            _fixedChainLogick.StopSpawnLogick();
            _cyclikChainLogick.StopSpawnLogick();
        }

        private void StartSpawnLogick()
        {
            _fixedChainLogick.StartSpawnLogick();
            _cyclikChainLogick.StartSpawnLogick();
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
                _fixedChainLogick.Execute();
                _cyclikChainLogick.Execute();
            }
        }

        #endregion
    }
}
