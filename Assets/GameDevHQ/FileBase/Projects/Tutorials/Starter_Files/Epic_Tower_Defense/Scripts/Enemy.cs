using UnityEngine;
using UnityEngine.AI;
using GameDevHQ.Scripts.Managers;
using System.Collections;

namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class Enemy : MonoBehaviour, IHealth
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _warfund;

        [SerializeField]
        private int _health;

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

        [Range(0,1)][SerializeField]
        float _dissolveSpeed;

        private Transform _target;
        private NavMeshAgent _agent;

        private int _ID;
        private bool _isAlive = true;
        private int _currentHealth;

        private void OnEnable()
        {
            //reset mech
            _isAlive = true;
            _currentHealth = Health;
            foreach (Renderer renderer in _renderers)
            {
                renderer.material.SetFloat("_Dissolve", 0);
            }

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
            //set ID when instantiated
            _ID = SpawnManager.Instance.GetNextID();
        }

        public void TakeDamage(int damage)
        {
            //take damage
            _currentHealth -= damage;
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

        IEnumerator Disabled()
        {
            yield return new WaitForSeconds(_cleanUpDelay);
            //run dissolve FX from Shader
            float count = 0;
            while (count * _dissolveSpeed <= 1)
            {
                foreach (Renderer renderer in _renderers)
                {
                    renderer.material.SetFloat("_Dissolve", count * _dissolveSpeed);
                }
                count += Time.deltaTime;
                yield return null;
            }
            //set back to walking before disabling
            _animator.WriteDefaultValues();
            this.gameObject.SetActive(false);
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