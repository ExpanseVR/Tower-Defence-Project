using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PoolManager is null.");
                }

                return null;
            }
        }

        [SerializeField]
        private List<ObjectPool> _objectPoolList = new List<ObjectPool>();

        private List<string> _objectPoolDescriptions = new List<string>();

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            //for each Object Pool, initialise their pools and store their names for search
            for (int i = 0; i < _objectPoolList.Count; i++)
            {
                if (_objectPoolList[i] != null)
                {
                    _objectPoolList[i].GenerateObjects();
                    _objectPoolDescriptions.Add(_objectPoolList[i].GetObjectPoolDescription());
                }
            }
        }

        //Find a specific Object Pool based on its description and return it
        public ObjectPool GetObjectPool (string objectPoolDescription)
        {
            for (int i = 0; i < _objectPoolList.Count; i++)
            {
                if (_objectPoolList[i].GetObjectPoolDescription() == objectPoolDescription)
                    return _objectPoolList[i];
            }
                return null;
        }
    }
}