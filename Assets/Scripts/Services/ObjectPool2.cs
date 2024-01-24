using UnityEngine;


namespace Dragoraptor
{
    public class ObjectPool2 : ObjectPool
    {
        public void PrepareObjects(PooledObject[] prefabs)
        {
            if (prefabs != null)
            {
                for (int i = 0; i < prefabs.Length; i++)
                {
                    PrepareObject(prefabs[i]);
                }
            }
        }

        public override PooledObject GetObjectOfType(string type)
        {
            if (!CheckIfTypeContains(type))
            {
                AddObjGroup(type);
            }

            return base.GetObjectOfType(type);
        }

        private void AddObjGroup(string type)
        {
            GameObject prefabGO = PrefabLoader.GetPrefab(type);
            if (prefabGO != null)
            {
                PooledObject obj = prefabGO.GetComponent<PooledObject>();
                if (obj != null)
                {
                    if (obj.Type.Equals(type))
                    {
                        PrepareObject(obj);
                    }
                }
            }
        }

    }
}
