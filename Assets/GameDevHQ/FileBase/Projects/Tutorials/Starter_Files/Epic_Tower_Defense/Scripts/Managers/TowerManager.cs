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
                //tower appears and follows mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    Debug.Log("Ray Hit");
                    Instantiate(_towerToPlace, rayHit.transform.position, Quaternion.identity);
                }

                //area of effect is red when not over predifinedArea
                //& predefined areas turn green
                //particle effect at available locactions
                foreach (GameObject area in _predifinedArea)
                {
                    area.SetActive(true); //change to event system
                }
            }

            //turret snaps to area when mouse over predefined area
            //left click to place tower
            //if enough warFunds

            //right click cancels placement
            //cant place on existing tower

            //support multiple tower types.

        }
    }
}