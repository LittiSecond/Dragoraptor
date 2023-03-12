using UnityEngine;
using System;
using System.Collections.Generic;


namespace Dragoraptor
{
    public sealed class NpcSpawner : IExecutable
    {

        #region Fields

        private readonly List<NpcBaseLogick> _npcList;
        private readonly List<string> _npcTypes;
        private NpcSpawnRule _spawnRule;
        private Action<NpcBaseLogick> _onDestroyListener;

        private float _timeCounter;
        private float _nextSpawnTime;
        private int _nextSpawnDataIndex;

        private bool _isEnabled;
        private bool _isSpawnDataRedy;
        private bool _isSpawnRuleFinished;

        #endregion


        #region ClassLifeCycles

        public NpcSpawner(List<NpcBaseLogick> list, Action<NpcBaseLogick> onDestroyListener)
        {
            _npcList = list;
            _npcTypes = new List<string>();
            _onDestroyListener = onDestroyListener;
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
            _isSpawnDataRedy = _spawnRule.SpawnDatas.Length > 0;

        }

        public void RestartNpcSpawn()
        {
            StopSpawnLogick();
            StartSpawnLogick();
        }

        private void StopSpawnLogick()
        {
            _isEnabled = false;
        }

        private void StartSpawnLogick()
        {
            if (_isSpawnDataRedy)
            {
                _timeCounter = 0.0f;
                _nextSpawnTime = _spawnRule.SpawnDatas[0].Time;
                _nextSpawnDataIndex = 0;
                _isSpawnRuleFinished = false;
                _isEnabled = true;
            }
        }

        private void SpawnNpc(int index, Vector2 position)
        {
            string npsType = _npcTypes[index];
            PooledObject obj = Services.Instance.ObjectPool.GetObjectOfType(npsType);
            if (obj != null)
            {
                NpcBaseLogick npc = obj as NpcBaseLogick;
                if (npc != null)
                {
                    npc.transform.position = position;
                    npc.OnDestroy += _onDestroyListener;
                    _npcList.Add(npc);
                    NpcData data = _spawnRule.SpawnDatas[_nextSpawnDataIndex].Data;
                    npc.SetAdditionalData(data);
                    float[] dataArray = _spawnRule.SpawnDatas[_nextSpawnDataIndex].FloatDatas;
                    npc.SetAdditionalDataArray(dataArray);
                    npc.Initialize();
                }
            }
        }

        #endregion


        #region IExecutable

        public void Execute()
        {
            if (_isEnabled && !_isSpawnRuleFinished)
            {
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= _nextSpawnTime)
                {
                    int npcIndex = _spawnRule.SpawnDatas[_nextSpawnDataIndex].PrefabIndex;
                    Vector2 pos = _spawnRule.SpawnDatas[_nextSpawnDataIndex].SpawnPosition;
                    SpawnNpc(npcIndex, pos);
                    _nextSpawnDataIndex++;
                    if (_nextSpawnDataIndex >= _spawnRule.SpawnDatas.Length)
                    {
                        _isSpawnRuleFinished = true;
                    }
                    else
                    {
                        _nextSpawnTime = _spawnRule.SpawnDatas[_nextSpawnDataIndex].Time;
                    }

                }

            }
        }

        #endregion
    }
}
