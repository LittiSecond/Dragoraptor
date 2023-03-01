using System;
using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewRule", menuName = "Resources/NpcSpawnRule")]
    public sealed class NpcSpawnRule : ScriptableObject
    {
        #region PrivateData

        [Serializable]
        public struct SpawnData
        {
            public int Index;
            public float Time;
            public Vector2 SpawnPosition;
        }

        #endregion


        #region Properties

        public NpcBaseLogick[] Prefabs;
        public SpawnData[] SpawnDatas;

        #endregion
    }
}
