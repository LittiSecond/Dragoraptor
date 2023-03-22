using UnityEngine;


namespace Dragoraptor 
{ 
    public static class PrefabLoader
    {
        #region Fields

        private const string PATH_PART = "Levels/Level";
        private const string SECOND_PATH_PART = "/Level";

        private static LevelDescriptor _loadedLevel;

        #endregion


        #region Methods



        public static GameObject GetPrefab(string prefabID)
        {
            GameObject go = null;

            if (PrefabPaths.Paths.ContainsKey(prefabID))
            {
                go = Resources.Load<GameObject>(PrefabPaths.Paths[prefabID]);
            }
            return go;
        }

        public static LevelDescriptor GetLevelDescriptor(int levelNumber)
        {
            LevelDescriptor newLevel = null;

            if (_loadedLevel != null)
            {
                if (levelNumber == _loadedLevel.LevelNumber)
                {
                    newLevel = _loadedLevel;
                }
            }

            if (newLevel == null)
            {
                string path = CreateFullPath(levelNumber);
                newLevel = Resources.Load<LevelDescriptor>(path);

                if (newLevel)
                {
                    if (_loadedLevel != null)
                    {
                        Resources.UnloadAsset(_loadedLevel);
                    }
                    _loadedLevel = newLevel;
                }
            }

            return newLevel;
        }

        private static string CreateFullPath(int levelNumber)
        {
            return PATH_PART + levelNumber.ToString() + SECOND_PATH_PART + levelNumber.ToString();
        }

        public static TextAsset LoadTextAsset(string assetID)
        {
            return Resources.Load(PrefabPaths.Paths[assetID], typeof(TextAsset)) as TextAsset;
        }

        #endregion
    }
}
