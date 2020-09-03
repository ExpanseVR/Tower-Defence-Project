using UnityEditorInternal;
using UnityEngine;

namespace GameDevHQ.Scripts
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T)); //JON <--- Need this or they keep coming up NULL.
                    Debug.Log(typeof(T).ToString() + " is NULL");
                }

                return _instance;
            }
        }

        private void AWAKE()
        {
            _instance = this as T;
        }
    }
}