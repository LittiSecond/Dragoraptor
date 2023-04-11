using UnityEngine;


namespace Dragoraptor 
{ 
    public static class PrefabLoader
    {
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

        public static TextAsset LoadTextAsset(string assetID)
        {
            return Resources.Load(PrefabPaths.Paths[assetID], typeof(TextAsset)) as TextAsset;
        }

        #endregion
    }
}
