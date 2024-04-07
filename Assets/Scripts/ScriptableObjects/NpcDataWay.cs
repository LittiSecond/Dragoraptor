using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewWay", menuName = "Resources/NpcDataWay")]
    public sealed class NpcDataWay : NpcData
    {
        public bool IsCyclic;
        public bool IsRelativeStartPosition;
        public Vector2[] Way;
    }
}
