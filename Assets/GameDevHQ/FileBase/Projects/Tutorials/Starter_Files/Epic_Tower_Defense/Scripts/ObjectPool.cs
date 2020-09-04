using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class ObjectPool
    {
        private List<GameObject> _objectPool = new List<GameObject>();

        public GameObject CheckForDisabledGameObject(GameObject objectTypeToFind)
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                //check if their is a disabled object
                if (_objectPool[i].activeSelf == false)
                {
                    //check if disable object is of type needed
                    if (_objectPool[i].gameObject.tag == objectTypeToFind.gameObject.tag)
                        return _objectPool[i];
                }
            }

            return null;
        }

        public void AddNewObject(GameObject newObject)
        {
            _objectPool.Add(newObject);
        }
    }
}