using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManger : MonoBehaviour
{
    //Reference to enemies to spawn
    [SerializeField]
    GameObject[] _enemies;

    [Range(1, 20)]
    [SerializeField]
    int _waveSize = 10;

    [SerializeField]
    int _numberOfWaves = 3;

    [SerializeField]
    float _delayBetweenSpawning = 5f;

    private int _currentWave = 1;
    private bool _waveStarted = false;

    private void Update()
    {
        //check if there is a wave going & if the max number of waves is reached
        if (_currentWave <= _numberOfWaves && _waveStarted == false)
        {
            //start wave
            print("starting wave " + _currentWave); //TO REMOVE
            _waveStarted = true;
            StartCoroutine(EnemyWave());
        }
    }

    IEnumerator EnemyWave ()
    {
        int i = 0;
        
        while (i < (_waveSize * _currentWave)) //wave size to increase per wave
        {
            //randomly select a random enemy
            int randomEnemy = Random.Range(0, _enemies.Length);
            //spawn the enemy
            SpawnManager.Instance.SpawnEnemy(_enemies[randomEnemy]);

            //have a delay between each enemy spawned 
            yield return new WaitForSeconds(_delayBetweenSpawning);
            i++;
        }

        _currentWave++;
        _waveStarted = false;
    }
}
