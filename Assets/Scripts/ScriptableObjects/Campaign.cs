using UnityEngine;


namespace Dragoraptor
{
    [CreateAssetMenu(fileName = "NewCampaign", menuName = "Resources/Campaign")]
    public sealed class Campaign : ScriptableObject
    {
        #region Fields

        public string[] LevelPaths;

        #endregion
    }
}