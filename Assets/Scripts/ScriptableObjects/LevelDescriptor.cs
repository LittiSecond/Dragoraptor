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
    }
}
