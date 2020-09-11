using GameDevHQ.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public class CameraBounds : MonoBehaviour
    {
        public static event Action onCameraBoundsHit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "CameraRig")
            {
                Debug.Log("CameraBounds :: OnTriggerEnter");
                if (onCameraBoundsHit != null)
                {
                    onCameraBoundsHit();
                }
            }
        }
    }
}