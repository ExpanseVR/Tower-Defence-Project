using GameDevHQ.Scripts.Managers;
using System.Collections;
using UnityEngine;

namespace GameDevHQ.Cam
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField]
        float _duration;

        [SerializeField]
        float _shakeMagnitude;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.EnemyGoalReached.ToString(), (System.Action<int>)StartShake);
        }

        public void StartShake (int notNeeded)
        { 
            StartCoroutine(Shake());
        }

        IEnumerator Shake ()
        {
            Vector3 originalPosition = transform.position;
            float currentMagnitude;
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                currentMagnitude = Mathf.Lerp(_shakeMagnitude, 0, elapsedTime / _duration);

                float x = originalPosition.x + Random.Range(-currentMagnitude, currentMagnitude);
                float z = originalPosition.z + Random.Range(-currentMagnitude, currentMagnitude);

                transform.position = new Vector3(x, originalPosition.y, z);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPosition;
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.EnemyGoalReached.ToString(), (System.Action<int>)StartShake);
        }
    }
}