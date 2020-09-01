using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameDevHQ.Scripts.Managers
{
    public class EnemyPoolManager : MonoBehaviour
    {
        private static EnemyPoolManager _instance;

        public static EnemyPoolManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("EnemyPoolManager is Null.");

                return null;
            }
        }

        [SerializeField]
        private GameObject[] _enemies;

        [SerializeField]
        private int _initialPoolSize = 10;

        private List<GameObject> _enemyPool;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            _enemyPool = GenerateEnemies(_initialPoolSize);
        }


        //generate a list of enemies to use
        List<GameObject> GenerateEnemies(int amountOfEnemies)
        {
            List<GameObject> tempList = new List<GameObject>(); //CHECK WITH JON ON THIS. CANT USE _enemypool

            for (int i = 0; i < amountOfEnemies; i++)
            {
                //instantiate gameObject, disable and cleanup by setting child to this gameobject and add to list
                GameObject enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)]);
                enemy.SetActive(false);
                enemy.transform.parent = this.transform;
                tempList.Add(enemy);
                //_enemyPool.Add(enemy);
            }
            return tempList;
            //return _enemyPool;
        }


        //check if their is a disabled enemy in the list and return enemy from list
        public GameObject CheckForDisabledGameObject()
        {
            for (int i = 0; i < _enemyPool.Count; i++)
            {

                if (_enemyPool[i].activeSelf == false)
                    return _enemyPool[i];
            }

            return null;
        }

        //add new enemy to list
        public void CreateNew(Transform positionToSpawn)
        {
            GameObject enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], positionToSpawn.position, Quaternion.identity);
            enemy.transform.parent = this.transform;
            _enemyPool.Add(enemy);
        }
    }
}