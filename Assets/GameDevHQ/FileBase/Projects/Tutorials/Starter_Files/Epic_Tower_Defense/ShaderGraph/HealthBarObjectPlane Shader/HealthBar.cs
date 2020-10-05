using UnityEngine;

namespace GameDevHQ.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private bool _faceCamera = true;

        private Camera _camera;
        private MaterialPropertyBlock _materialPropertyBlock;

        private void Start()
        {
            InitiateHealth();
            _camera = Camera.main;
        }

        private void InitiateHealth()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetFloat("_HealthInput", 100);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        void Update()
        {
            FaceCamera();
        }

        private void FaceCamera()
        {
            if (_faceCamera == false)
                return;

            //face camera
            Vector3 directionToFace = (transform.position - _camera.transform.position);
            transform.rotation = Quaternion.LookRotation(directionToFace, Vector3.up);
        }

        public bool SetHealthBar(float health)
        {
            if (health >= 0 || health <= 100)
            {
                //change only this instance of material
                _renderer.GetPropertyBlock(_materialPropertyBlock);
                _materialPropertyBlock.SetFloat("_HealthInput", health);
                _renderer.SetPropertyBlock(_materialPropertyBlock);
                return true;
            }
            else
            {
                Debug.LogError("HealthBar :: SetHealthBar - health input out of range");
                return false;
            }
        }
    }
}