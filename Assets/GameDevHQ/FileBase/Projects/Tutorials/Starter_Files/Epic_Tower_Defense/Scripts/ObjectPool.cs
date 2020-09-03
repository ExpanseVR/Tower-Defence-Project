using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class ObjectPool
    {
        private List<GameObject> _objectPool = new List<GameObject>();

        //create new pool
        public ObjectPool CreateNewPool(List<GameObject> objectList)
        {
            this._objectPool = objectList;

            //return the new pool
            return this;
        }

        //check if their is a disabled object in the list and if so return the first one
        public GameObject CheckForDisabledGameObject()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {

                if (_objectPool[i].activeSelf == false)
                    return _objectPool[i];
            }

            return null;
        }

        public void AddNewObject(GameObject newObject)
        {
            _objectPool.Add(newObject);
        }
    }
}