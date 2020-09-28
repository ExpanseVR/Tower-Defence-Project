using GameDevHQ.Scripts.Managers;
using UnityEngine;

namespace GameDevHQ.UI
{
    public class RestartButtonUI : MonoBehaviour
    {
        public void RestartButtonPressed ()
        {
            GameSceneManager.Instance.RestartLevel();
        }
    }
}