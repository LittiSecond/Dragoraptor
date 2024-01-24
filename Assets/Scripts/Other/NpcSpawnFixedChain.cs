using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcSpawnFixedChain
    {
        
        private readonly INpcSpawner _npcSpawner;
        private SpawnData[] _spawnDatas;

        private float _timeCounter;
        private float _nextSpawnTime;
        private int _nextSpawnDataIndex;

        private bool _isSpawnRuleFinished;
        private bool _isSpawnDataReady;


        public NpcSpawnFixedChain(INpcSpawner npcSpawner)
        {
            _npcSpawner = npcSpawner;
        }


        public void SetSpawnData(SpawnData[] spawnDatas)
        {
            StopSpawnLogick();
            _spawnDatas = spawnDatas;
            _isSpawnDataReady = false;
            if (spawnDatas != null)
            {
                if (spawnDatas.Length > 0)
                {
                    _isSpawnDataReady = true;
                }
            }
        }

        public void StartSpawnLogick()
        {
            if (_isSpawnDataReady)
            {
                _timeCounter = 0.0f;
                _nextSpawnTime = _spawnDatas[0].Time;
                _nextSpawnDataIndex = 0;
                _isSpawnRuleFinished = false;
            }
        }

        public void StopSpawnLogick()
        {
            _isSpawnRuleFinished = true;
        }

        public  void Execute()
        {
            if (!_isSpawnRuleFinished)
            {
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= _nextSpawnTime)
                {
                    _npcSpawner.SpawnNpc(_spawnDatas[_nextSpawnDataIndex]);
                    _nextSpawnDataIndex++;
                    if (_nextSpawnDataIndex >= _spawnDatas.Length)
                    {
                        _isSpawnRuleFinished = true;
                    }
                    else
                    {
                        _nextSpawnTime = _spawnDatas[_nextSpawnDataIndex].Time;
                    }
                }
            }
        }

    }
}