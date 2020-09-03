using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] _enemies;

        public GameObject[] EnemyTypes()
        {
            return _enemies;
        }
    }
}