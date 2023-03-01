using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewRule", menuName = "Resources/NpcSpawnRule")]
    public sealed class NpcSpawnRule : ScriptableObject
    {
        public NpcBaseLogick[] Prefabs;
        public SpawnData[] SpawnDatas;
    }
}
