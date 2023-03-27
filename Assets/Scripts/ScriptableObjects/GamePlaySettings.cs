﻿using UnityEngine;


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
        public int AttackPower;
        public int MaxHealth;
        public int Armor;
        public AttackAreasPack AttackAreas;
        public int MaxSatiety;
        public float VictoryScoreMultipler = 2.0f;
        public float DefeatScoreMultipler = 1.0f;
        public float NullSatietyScoreMultipler = 0.5f;
        public float SatietySuccefScoreMultipler = 1.0f;
    }
}
