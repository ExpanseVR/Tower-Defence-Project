using GameDevHQ.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class CameraBounds : MonoBehaviour
    {
        public static Action CameraBoundsHit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CameraManager>() != null)
            {
                Debug.Log("CameraBounds :: OnTriggerEnter");
                if (CameraBoundsHit != null)
                {
                    CameraBoundsHit();
                }
            }
        }
    }
}