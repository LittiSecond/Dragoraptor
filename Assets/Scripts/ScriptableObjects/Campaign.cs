using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewCampaign", menuName = "Resources/Campaign")]
    public sealed class Campaign : ScriptableObject
    {

        public string[] LevelPaths;

    }
}