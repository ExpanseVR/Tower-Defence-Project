using GameDevHQ.Scripts.Utility;
using UnityEngine.SceneManagement;


namespace GameDevHQ.Scripts.Managers
{
    public class GameSceneManager : MonoSingleton<GameSceneManager>
    {
        private int _currentSceneIndex;

        private void OnEnable()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(_currentSceneIndex);
        }
    }
}