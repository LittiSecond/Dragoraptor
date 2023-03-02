using System.Collections.Generic;
using UnityEngine;


namespace Dragoraptor
{
    public class ObjectPool
    {
        #region PrivateData

        private struct ObjGroup
        {
            public string _type;
            public PooledObject _prefab;
            public List<PooledObject> ObjectsList;
            public ObjGroup(string type)
            {
                _type = type;
                _prefab = null;
                ObjectsList = new List<PooledObject>();
            }
        }

        #endregion


        #region Fields

        private const int CODE_NOT_EXIST_TYPE = -1;
        
        private readonly List<ObjGroup> _objectsGroupsList;

        #endregion


        #region ClassLifeCycles

        public ObjectPool()
        {
            _objectsGroupsList = new List<ObjGroup>();
        }

        #endregion


        #region Methods

        public void ReturnToPool(PooledObject pooledObject)
        {
            if (pooledObject != null)
            {
                if (pooledObject.ObjectPool == this)
                {
                    string type = pooledObject.Type;

                    int index = GetIndexGroupOfType(type);
                    if (index > CODE_NOT_EXIST_TYPE)
                    {
                        if (_objectsGroupsList[index].ObjectsList.Contains(pooledObject))
                        {
                            ResetState(pooledObject);
                        }
                    }
                }

            }
        }

        public PooledObject GetObjectOfType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }

            PooledObject obj = null;

            int index = GetIndexGroupOfType(type);
            if (index != CODE_NOT_EXIST_TYPE)
            {
                ObjGroup group = _objectsGroupsList[index];
                List<PooledObject> objList = group.ObjectsList;
                for (int i = 0; i < objList.Count; i++)
                {
                    PooledObject skript = objList[i];
                    if (!skript.IsUsed)
                    {
                        skript.IsUsed = true;
                        obj = skript;
                        obj.gameObject.SetActive(true);
                        break;
                    }
                }

                if (obj == null)
                {
                    AddObject(group);
                    PooledObject skript = objList[objList.Count - 1];
                    if (!skript.IsUsed)
                    {
                        skript.IsUsed = true;
                        obj = skript;
                        obj.gameObject.SetActive(true);
                    }
                }
            }
            return obj;
        }

        public void PrepareObjects(PooledObject[] prefabs )
        {
            if (prefabs != null)
            {
                for (int i = 0; i < prefabs.Length; i++)
                {
                    PrepareObject(prefabs[i]);
                }
            }
        }

        public void PrepareObject(PooledObject prefab)
        {
            if (prefab)
            {
                string type = prefab.Type;
                if (GetIndexGroupOfType(type) == CODE_NOT_EXIST_TYPE)
                {
                    ObjGroup group = new ObjGroup(type);
                    group._prefab = prefab;
                    _objectsGroupsList.Add(group);
                }
            }
        }

        public void Clear()
        {
            for (int i = _objectsGroupsList.Count - 1; i >= 0; i--)
            {
                ObjGroup group = _objectsGroupsList[i];
                List<PooledObject> objectsList = group.ObjectsList;
                for (int j = objectsList.Count - 1; j >= 0; j--)
                {
                    PooledObject obj = objectsList[j];
                    if (!obj.IsUsed)
                    {
                        GameObject.Destroy(obj.gameObject);
                        objectsList.RemoveAt(j);
                    }
                }

                if (objectsList.Count == 0)
                {
                    group._prefab = null;
                    _objectsGroupsList.RemoveAt(i);
                }
            }
        }

        public void ReturnAllToPool()
        {
            int groupQuantity = _objectsGroupsList.Count;
            for (int i = 0; i < groupQuantity; i++)
            {
                List<PooledObject> list = _objectsGroupsList[i].ObjectsList;
                int objQuantity = list.Count;
                for (int j = 0; j < objQuantity; j++)
                {
                    PooledObject po = list[j];
                    if (po)
                    {
                        if (po.IsUsed)
                        {
                            po.PrepareToReturnToPool();
                            ResetState(po);
                        }
                    }
                }
            }
        }

        private int GetIndexGroupOfType(string type)
        {
            for (int i = 0; i < _objectsGroupsList.Count; i++)
            {
                if (_objectsGroupsList[i]._type.Equals(type)) return i;
            }

            return CODE_NOT_EXIST_TYPE;
        }

        private PooledObject AddObject(ObjGroup group)
        {
            PooledObject prefab = group._prefab;

            PooledObject newObject = GameObject.Instantiate<PooledObject>(prefab);
            if (newObject)
            {
                group.ObjectsList.Add(newObject);
                newObject.SetObjectPool(this);
                ResetState(newObject);
            }

            return newObject;
        }

        private void ResetState(PooledObject obj)
        {
            obj.IsUsed = false;
            obj.gameObject.SetActive(false);
        }

        #endregion
    }
}
