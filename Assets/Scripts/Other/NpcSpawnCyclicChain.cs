using System;
using UnityEngine;


namespace Dragoraptor
{
    public sealed class NpcSpawnCyclicChain
    {
        //
        // Все классы NpcSpawn принимают решения: когда порождать какого моба и отдаёт команду на его порождение
        //
        // Класс NpcSpawnCyclicChain в качестве ИД принимает массив SpawnData (содержит мобов с ИД для каждого моба) 
        // и длительность цикла.
        // Класс должен распределить спавн мобов в отрезке времени начиная
        // с момента старта логики (с момента вызова функции StartSpawnLogic()) и длительностью цикла.
        // После окончания цикла, начать цикл заново.
        //
        // Повторять цикл пока не получит команду на остановку работы - вызов функции StopSpawnLogic()
        //

        private readonly INpcSpawner _npcSpawner;

        private SpawnData[] _spawnDatas;


        private float _spawnTimeCounter;
        private float _cycleTimeCounter;
        private float _nextSpawnTime;
        private float _cycleDuration;
        private int _nextSpawnDataIndex;

        private bool _isSpawnRuleFinished;
        private bool _isSpawnDataReady;


        public NpcSpawnCyclicChain(INpcSpawner npcSpawner)
        {
            _npcSpawner = npcSpawner;
        }


        public void SetSpawnData(SpawnData[] spawnDatas, float cycleDuration)
        {
            StopSpawnLogic();
            _isSpawnDataReady = false;
            if (spawnDatas != null)
            {
                if (spawnDatas.Length > 0)
                {
                    if (_spawnDatas == null)
                    {
                        _spawnDatas = new SpawnData[spawnDatas.Length];
                    }
                    else if (_spawnDatas.Length != spawnDatas.Length)
                    {
                        _spawnDatas = new SpawnData[spawnDatas.Length];
                    }

                    CopyRandomizeSpawnDatas(spawnDatas);
                    RandomizeSpawnTimes(cycleDuration);

                    _isSpawnDataReady = true;
                }
            }
        }

        public void StartSpawnLogic()
        {
            if (_isSpawnDataReady)
            {
                _spawnTimeCounter = 0.0f;
                _nextSpawnTime = _spawnDatas[0].Time;
                _nextSpawnDataIndex = 0;
                _isSpawnRuleFinished = false;
                _cycleTimeCounter = 0.0f;
            }
        }

        public void StopSpawnLogic()
        {
            _isSpawnRuleFinished = true;
        }

        public void Execute()
        {
            if (!_isSpawnRuleFinished)
            {
                float deltaTime = Time.deltaTime;
                _spawnTimeCounter += deltaTime;
                if (_spawnTimeCounter >= _nextSpawnTime)
                {
                    _npcSpawner.SpawnNpc(_spawnDatas[_nextSpawnDataIndex]);
                    //Debug.Log($"NpcSpawnCyclicChain->Execute: _nextSpawnDataIndex = {_nextSpawnDataIndex} ");
                    _nextSpawnDataIndex++;
                    if (_nextSpawnDataIndex >= _spawnDatas.Length)
                    {
                        _nextSpawnTime = float.MaxValue;
                    }
                    else
                    {
                        _nextSpawnTime = _spawnDatas[_nextSpawnDataIndex].Time;
                    }
                }
                
                _cycleTimeCounter += deltaTime;
                if (_cycleTimeCounter >= _cycleDuration)
                {
                    RandomizeSpawnDatas();
                    RandomizeSpawnTimes(_cycleDuration);
                    StartSpawnLogic();
                }
                
            }
        }

        private void CopyRandomizeSpawnDatas(SpawnData[] spawnDatas)
        {
            int quantity = _spawnDatas.Length;
            bool[] isMoved = new bool[quantity];
            for (int i = 0; i < quantity; i++)
            {
                int index = UnityEngine.Random.Range(0, quantity);
                while (isMoved[index])
                {
                    index++;
                    if (index >= quantity)
                    {
                        index = 0;
                    }
                }
                _spawnDatas[i] = spawnDatas[index];
                isMoved[index] = true;
            }
        }

        private void RandomizeSpawnDatas()
        {
            SpawnData[] tempDatas = new SpawnData[_spawnDatas.Length];
            for (int i = 0; i < _spawnDatas.Length; i++)
            {
                tempDatas[i] = _spawnDatas[i];
            }
            CopyRandomizeSpawnDatas(tempDatas);
        }

        private void RandomizeSpawnTimes(float newCycleDuration)
        {
            int quantity = _spawnDatas.Length;
            float stepInterval = newCycleDuration / (quantity + 1);
            float previousTime = -1.0f;

            for (int i = 0; i < quantity; i++)
            {
                float nodalTime = stepInterval * (i + 1);
                float dt = UnityEngine.Random.Range(-stepInterval, stepInterval);
                float currentTime = nodalTime + dt;
                if ( i > 0 )
                {
                    if (currentTime < previousTime)
                    {
                        _spawnDatas[i - 1].Time = currentTime;
                        currentTime = previousTime;
                    }
                }
                _spawnDatas[i].Time = currentTime;
                previousTime = currentTime;
            }
            _cycleDuration = newCycleDuration;
            //ThrowDebugInfo();
        }

        private void ThrowDebugInfo()
        {
            string message = _cycleDuration.ToString();
            for (int i = 0; i < _spawnDatas.Length; i++)
            {
                message += ";    " + _spawnDatas[i].Time.ToString();
            }

            Debug.Log("NpcSpawnCyclicChain->ThrowDebugInfo: " + message);
        }

    }
}