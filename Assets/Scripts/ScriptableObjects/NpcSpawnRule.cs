using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewRule", menuName = "Resources/NpcSpawnRule")]
    public sealed class NpcSpawnRule : ScriptableObject
    {
        public NpcBaseLogick[] Prefabs;
        [Header("Fixed chain")]
        public SpawnData[] SpawnDatas;
        [Header("Cyclic randomized chain")]
        public SpawnData[] CyclicSpawnDatas;
        public float CycleDuration = 10.0f;
    }
}
