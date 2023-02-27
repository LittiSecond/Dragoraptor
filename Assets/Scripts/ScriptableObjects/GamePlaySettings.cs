using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewSettings", menuName = "Resources/GamePlaySettings")]
    public sealed class GamePlaySettings : ScriptableObject
    {
        public float MinJumpForce;
        public float MaxJumpForce;
        public float NoJumpPowerIndicatorLength;
        public float MaxJumpPowerIndicatorLength;
        public float WalkSpeed;
        public Vector2 CharacterSpawnPosition;
    }
}
