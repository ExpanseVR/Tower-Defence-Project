using GameDevHQ.Scripts;
using GameDevHQ.Scripts.Managers;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevHQ.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField]
        private float _horizontalAdjustment;
        
        [SerializeField]
        private float _verticalAdjustment;

        [SerializeField]
        private Slider _healthSlider;

        [SerializeField]
        private Image _healthColour;

        [SerializeField]
        private Color _fullHealth;

        [SerializeField]
        private Color _mediumHealth;

        [SerializeField]
        private Color _lowHealth;

        [SerializeField]
        private Color _lowHealthFlash;

        [SerializeField]
        private float _flashSpeed;

        private float _maxHealth;
        private float _currentHealth;
        private bool _healthIsLow = false;
        private Enemy _myEnemy;
        private RectTransform _canvasRectT;

        private void OnEnable()
        {
            EventManager.Listen(EventManager.Events.EnemyDamaged.ToString(), (Action<Enemy>)UpdateHealthBar);
        }

        // Update is called once per frame
        void Update()
        {
            SetMenuPosition();
        }

        public void UpdateHealthBar(Enemy damagedEnemy)
        {

            if (damagedEnemy != _myEnemy)
                return;

            _currentHealth = _myEnemy.GetHealth();
            _healthSlider.value = _currentHealth;
            if (_currentHealth > (_maxHealth / 100 * 60))
            {
                _healthColour.color = _fullHealth;
                return;
            }
            if (_currentHealth > (_maxHealth / 100 * 25))
                _healthColour.color = _mediumHealth;
            if (_currentHealth <= (_maxHealth / 100 * 25))
            {
                if (!_healthIsLow)
                    StartCoroutine(LowHealth());
            }
            if (_currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void SetMenuPosition()
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main,_myEnemy.transform.position);
            screenPoint.y += _horizontalAdjustment;
            screenPoint.x += _verticalAdjustment;
            GetComponent<RectTransform>().anchoredPosition = screenPoint - _canvasRectT.sizeDelta / 2;    
        }

        IEnumerator LowHealth ()
        {
            bool isDark = true;
            _healthIsLow = true;

            while (_currentHealth <= (_maxHealth / 100 * 25))
            {
                if (isDark)
                    _healthColour.color = _lowHealth;
                else
                    _healthColour.color = _lowHealthFlash;

                isDark = !isDark;
                yield return new WaitForSeconds(_flashSpeed);
            }

            _healthIsLow = false;
        }

        public void SetEnemyTransform (Enemy myEnemy, RectTransform canvasRectT)
        {
            this._canvasRectT = canvasRectT;
            this._myEnemy = myEnemy;
            _maxHealth = myEnemy.GetHealth();
            UpdateHealthBar(myEnemy);
            _healthSlider.maxValue = _currentHealth;
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.EnemyDamaged.ToString(), (Action<Enemy>)UpdateHealthBar);
        }
    }
}