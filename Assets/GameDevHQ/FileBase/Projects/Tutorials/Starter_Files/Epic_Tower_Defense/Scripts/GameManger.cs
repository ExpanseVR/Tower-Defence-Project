using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    //Reference to enemies to spawn
    [SerializeField]
    GameObject[] _enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        //start wave
        //have a delay between each enemy spawned
        //for each amount in wave
        //randomly select a random enemy
        //spawn the enemy
        SpawnManager.Instance.SpawnEnemy(_enemies[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
