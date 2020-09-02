using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private string _objectPoolDescription;

        [SerializeField]
        private int _initialPoolSize;

        [SerializeField]
        private GameObject[] _objectTypes;

        private List<GameObject> _objectPool = new List<GameObject>();


        //generate a list of enemies to use
        public void GenerateObjects()
        {
            List<GameObject> tempList = new List<GameObject>(); //CHECK WITH JON ON THIS. CANT USE _enemypool

            for (int i = 0; i < _initialPoolSize; i++)
            {
                //instantiate gameObject, disable and cleanup by setting child to this gameobject and add to list
                GameObject newObject = Instantiate(_objectTypes[Random.Range(0, _objectTypes.Length)]);
                newObject.SetActive(false);
                newObject.transform.parent = this.transform;
                tempList.Add(newObject);
                _objectPool.Add(newObject);
            }
        }

        public string GetObjectPoolDescription()
        {
            return _objectPoolDescription;
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

        //add new object to list
        public void CreateNew(Transform positionToSpawn)
        {
            GameObject randomObject = _objectTypes[Random.Range(0, _objectTypes.Length)];
            GameObject newObject = Instantiate(randomObject, positionToSpawn.position, Quaternion.identity);
            newObject.transform.parent = this.transform;
            _objectPool.Add(newObject);
        }
    }
}