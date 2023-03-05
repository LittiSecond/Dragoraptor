using System;
using UnityEngine;


namespace Dragoraptor
{
    [Serializable]
    public struct SpawnData
    {
        public int PrefabIndex;
        public float Time;
        public Vector2 SpawnPosition;
        public NpcData Data;
    }
}
