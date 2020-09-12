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

        public void ButtonPressed ()
        {
            UIManager.Instance.ArmorButton(_towerToSpawn);
        }
    }
}