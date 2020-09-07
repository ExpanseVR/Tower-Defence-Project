using GameDevHQ.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameDevHQ.Scripts.Managers
{
    public class TowerManager : MonoSingleton<TowerManager>
    {
        [SerializeField]
        GameObject _towerToPlace;

        [SerializeField]
        GameObject[] _predifinedArea;

        [SerializeField]
        Material _nonPredefined_MAT;

        // Update is called once per frame
        void Update()
        {
            //when build option is selected
            if (Input.GetKeyDown(KeyCode.T))
            {
                //terrain is turned red???

                //& predefined areas turn green
                foreach (GameObject area in _predifinedArea)
                {
                    area.SetActive(true);
                }
            }
            //particle effect at available locactions

            //turret appears when mouse over predefined area
            //left click to place tower
            //if enough warFunds

            //right click cancels placement
            //cant place on existing tower

            //support multiple tower types.

        }
    }
}