using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Transform _spawnHeirarchyLocation;
    
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The GameManager is NULL.");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SpawnEnemy (GameObject enemyToSpawn)
    {
        //instantiate enemy
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, _spawnPoint.position, Quaternion.identity);

        //change parent in Hierarchy to tidy up
        spawnedEnemy.transform.parent = _spawnHeirarchyLocation;
    }
}
