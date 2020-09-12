using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class ObjectPool
    {
        private List<GameObject> _objectPool = new List<GameObject>();

        public GameObject GetGameObjectFromPool(GameObject objectTypeToFind)
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                //check if there is a disabled object
                if (_objectPool[i].activeSelf == false)
                {
                    //check if disable object is of type needed
                    if (_objectPool[i].gameObject.CompareTag(objectTypeToFind.gameObject.tag))
                        return _objectPool[i];
                }
            }
                        
            //Instantiate Object if not in Pool, add it to the pool and Return the object
            GameObject newObject = UnityEngine.Object.Instantiate(objectTypeToFind); //ASK JON <--- simplifying from GameObject does it matter?
            AddNewObject(newObject);
            return newObject;
        }

        private void AddNewObject(GameObject newObject)
        {
            _objectPool.Add(newObject);
        }


    }
}