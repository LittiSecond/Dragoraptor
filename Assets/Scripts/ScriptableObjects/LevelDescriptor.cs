using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Resources/LevelDescriptor")]
    public sealed class LevelDescriptor : ScriptableObject
    {
        [Range(1,100)]
        public int LevelNumber;
        public GameObject GroundPrefab;
        public GameObject BackgroundPrefab;
        public NpcSpawnRule SpawnRule;
        [Range(0,1.0f), Tooltip("relative maximum satiety")]
        public float SatietyToSucces = 0.75f;
        public float LevelDuration = 180.0f;
    }
}
