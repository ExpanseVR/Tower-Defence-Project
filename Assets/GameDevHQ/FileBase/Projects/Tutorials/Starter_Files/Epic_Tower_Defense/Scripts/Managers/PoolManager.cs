using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private List<ObjectPool> _objectPoolList = new List<ObjectPool>();

        public int CreateNewList(GameObject[] objectTypes, int initialPoolSize)
        {
            ObjectPool newPool = new ObjectPool();
            List<GameObject> tempList = new List<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                //instantiate gameObject, disable and cleanup by setting child to this gameobject and add to list
                GameObject newObject = Instantiate(objectTypes[Random.Range(0, objectTypes.Length)]);
                newObject.SetActive(false);
                newObject.transform.parent = this.transform;
                tempList.Add(newObject);
            }

            _objectPoolList.Add(newPool.CreateNewPool(tempList));

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
        public GameObject AddToExistingList(int listReference, GameObject[] objectTypes)
        {
            GameObject randomObject = objectTypes[Random.Range(0, objectTypes.Length)];
            GameObject newObject = Instantiate(randomObject);
            newObject.transform.parent = this.transform;
            _objectPoolList[listReference].AddNewObject(newObject);

            return newObject;
        }
    }
}