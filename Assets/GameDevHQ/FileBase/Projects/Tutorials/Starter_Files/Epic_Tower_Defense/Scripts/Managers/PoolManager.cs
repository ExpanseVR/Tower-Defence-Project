using GameDevHQ.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        public List<ObjectPool> _objectPoolList = new List<ObjectPool>();

        public int CreateNewList()
        {
            ObjectPool newPool = new ObjectPool();
            _objectPoolList.Add(newPool);

            return _objectPoolList.Count -1;
        }

        //return objectpool based on reference
        public ObjectPool ReturnPool(int poolReference)
        {
            if (_objectPoolList[poolReference] != null)
                return _objectPoolList[poolReference];
            else
            {
                Debug.LogError("Pool reference number for _objectPoolList is out of range.");
                return null;
            }
        }

        //instantiate new object, add to the pool and return the new object
        public GameObject AddToExistingList(int listReference, GameObject objectToAdd)
        {
            GameObject newObject = Instantiate(objectToAdd);
            newObject.transform.parent = this.transform;
            _objectPoolList[listReference].AddNewObject(newObject);

            return newObject;
        }
    }
}