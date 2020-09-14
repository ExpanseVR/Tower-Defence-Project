using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.Scripts.Managers;
using UnityEngine.UI;

namespace GameDevHQ.Scripts

{
    public class ArmoryButton : MonoBehaviour
    {
        [SerializeField]
        GameObject _towerToSpawn;

        int cost;

        private void Start()
        {
            cost = _towerToSpawn.GetComponent<Tower>().WarFundsRequired();
        }

        public void ButtonPressed ()
        {
            UIManager.Instance.ArmorButton(_towerToSpawn);
        }
    }
}