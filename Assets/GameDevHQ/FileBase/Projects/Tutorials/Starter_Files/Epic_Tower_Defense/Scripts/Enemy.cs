using UnityEngine;
using UnityEngine.AI;
using GameDevHQ.Scripts.Managers;
using System.Collections;
using GameDevHQ.UI;

namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class Enemy : MonoBehaviour, IHealth
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _warfundReward;

        [SerializeField]
        private int _playerDamage;

        [SerializeField]
        private int _health;

        [SerializeField]
        private HealthBar _healthBar;

        public int Health { get => _health; set => _health = value; }

        [SerializeField]
        ParticleSystem _damageFX;

        [SerializeField]
        ParticleSystem _deathFX;

        [SerializeField]
        Animator _animator;

        [SerializeField]
        float _cleanUpDelay;

        [SerializeField]
        Renderer[] _renderers;

        private MaterialPropertyBlock _materialPropertyBlock;

        [Range(0,1)][SerializeField]
        float _dissolveSpeed;

        private Transform _target;
        private NavMeshAgent _agent;

        private int _ID;
        private bool _isAlive = true;
        private int _currentHealth;
        private bool _hasBeenSet = false;

        private void OnEnable()
        {
            //reset mech
            _isAlive = true;
            _currentHealth = Health;
            if (_hasBeenSet)
                _healthBar.SetHealthBar(100);
            else
                _hasBeenSet = true;

            //get target
            _target = GameManger.Instance.RequestTarget();

            //move to target
            _agent = GetComponent<NavMeshAgent>();
            if (_agent != null)
            {
                _agent.speed = _speed;
                _agent.SetDestination(_target.position);
            }
        }

        private void Start()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            //set ID when instantiated
            _ID = SpawnManager.Instance.GetNextID();
        }

        public void TakeDamage(int damage)
        {
            //take damage
            _currentHealth -= damage;
            _healthBar.SetHealthBar(100 / _health * _currentHealth);
            //play damage FX
            if (_damageFX != null)
                _damageFX.Play();
            //check to see if health is zero or less
            if (_currentHealth <= 0)
                Destroyed();
        }

        private void Destroyed ()
        {
            //if destroyed play death FX
            if (_deathFX != null && _isAlive)
            {
                _deathFX.Play();
                _isAlive = false;
                //stop movement
                _agent.isStopped = true;
                //play death animation
                _animator.SetBool("IsShooting", false);
                _animator.SetBool("IsAlive", false);
                //and disable after x seconds
                StartCoroutine(Disabled());
            }
        }

        public int GetPlayerDamage()
        {
            return _playerDamage;
        }

        IEnumerator Disabled()
        {
            yield return new WaitForSeconds(_cleanUpDelay);
            //run dissolve FX from Shader
            float count = 0;
            while (count * _dissolveSpeed <= 1)
            {
                foreach (Renderer renderer in _renderers)
                {
                    renderer.GetPropertyBlock(_materialPropertyBlock);
                    _materialPropertyBlock.SetFloat("_Dissolve", count * _dissolveSpeed);
                    renderer.SetPropertyBlock(_materialPropertyBlock);
                }
                count += Time.deltaTime;
                yield return null;
            }
            //set back to walking and visible before disabling
            _animator.WriteDefaultValues();
            foreach (Renderer renderer in _renderers)
            {
                renderer.GetPropertyBlock(_materialPropertyBlock);
                _materialPropertyBlock.SetFloat("_Dissolve", 0);
                renderer.SetPropertyBlock(_materialPropertyBlock);
            }
            EventManager.Fire(EventManager.Events.EnemyCleanUp.ToString(), _warfundReward);
            this.gameObject.SetActive(false);
        }

        public int GetHealth()
        {
            return _currentHealth;
        }

        public int GetID ()
        {
            return _ID;
        }

        public bool IsAlive ()
        {
            return _isAlive;
        }    
    }
}