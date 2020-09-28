using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevHQ.FileBase.Missle_Launcher;
using GameDevHQ.Scripts;

//namespace GameDevHQ.FileBase.Missle_Launcher.Missle
namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(Rigidbody))] //require rigidbody
    [RequireComponent(typeof(AudioSource))] //require audiosource
    public class Missle : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particle; //reference to the particle system
        [SerializeField]
        private GameObject _explosionPrefab; //Explosion prefab to play on impact
        [SerializeField]
        private float _launchSpeed; //launch speed of the rocket
        [SerializeField]
        private float _power; //power of the rocket
        [SerializeField] //fuse delay of the rocket
        private float _fuseDelay;

        private Rigidbody _rigidbody; //reference to the rigidbody of the rocket
        private AudioSource _audioSource; //reference to the audiosource of the rocket

        public float _initialLaunchTime = 2.0f; //initial launch time for the rocket
        public float time;
        private bool _thrust; //bool to enable the rocket thrusters

        private bool _trackRotation = false; //bool to track rotation of the rocket

        private Tower.MissileType _missileType;
        private Transform _target;

        private GameObject _explosionFX;
        private bool _explosionFXSet = false;
        private int _missileDamage;

       
        private void OnEnable()
        {
            _thrust = false; //set thrust bool to false
            _initialLaunchTime = Time.time; //set the initial launch time
        }

        // Use this for initialization
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>(); //assign the rigidbody component 
            _audioSource = GetComponent<AudioSource>(); //assign the audiosource component
            _audioSource.pitch = Random.Range(0.7f, 1.9f); //randomize the pitch of the rocket audio
            _particle.Play(); //play the particles of the rocket
            _audioSource.Play(); //play the rocket sound
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            time = Time.time;

            _rigidbody.AddForce(transform.forward * _launchSpeed); //add force to the rocket in the forward direction

            if (Time.time > _initialLaunchTime + _fuseDelay) //check if the initial launch + fuse delay has passed
            {
                _thrust = true; //thrust bool goes true
            }

            if (_thrust == true) //if thrust is true
            {
                _rigidbody.useGravity = true; //enable gravity 
                _rigidbody.velocity = transform.forward * _power; //set velocity multiplied by the power variable
                _thrust = false; //set thrust bool to false
                _trackRotation = true; //track rotation bool set to true
            }

            if (_trackRotation == true) //check track rotation bool
            {
                if (_missileType == Tower.MissileType.Normal) //checking for normal missile 
                {
                    _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity); // adjust rotation of rocket based on velocity
                    _rigidbody.AddForce(transform.forward * 100f); //add force to the rocket
                }
                else if (_missileType == Tower.MissileType.Homing) //if missle is homing
                {
                    Vector3 direction = _target.position - transform.position; //calculate direciton for rocket to face
                    direction.Normalize(); //set the magnitude of the vector to 1
                    Vector3 turnAmount = Vector3.Cross(transform.forward, direction); //using cross product, we multiply our forward vector of the rocket by the direction vector, to create a perpendular vector, which specifies the turn amount

                    _rigidbody.angularVelocity = turnAmount * _power; //apply angular velocity
                    _rigidbody.velocity = transform.forward * _power; //apply forward velocity

                }
            }

        }

        /// <summary>
        /// This method is used to assign traits to our missle assigned from the launcher.
        /// </summary>
        public void AssignMissleRules(Missle_Launcher.MissileType missileType, Transform target, float launchSpeed, float power, float fuseDelay, int missileDamage)
        {
            _missileType = missileType; //assign the missle type
            _target = target; //Who should the rocket follow?
            _launchSpeed = launchSpeed; //set the launch speed
            _power = power; //set the power
            _fuseDelay = fuseDelay; //set the fuse delay
            _missileDamage = missileDamage;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "CrabMech" || other.gameObject.tag == "RunMech")
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(_missileDamage);

                if (_explosionPrefab != null)
                {
                    if (_explosionFXSet == false)
                    {
                        _explosionFX = Instantiate(_explosionPrefab, transform.position, Quaternion.identity); //instantiate explosion
                        _explosionFX.transform.parent = this.transform.parent.transform; //set the parent of the FX to the parent of the missile
                        _explosionFXSet = true;
                    }
                    else
                    {
                        _explosionFX.transform.position = this.transform.position;
                        _explosionFX.SetActive(true);
                    }
                }

                Cleanup();
            }
        }

        private void Cleanup()
        {
            //cleanup the rocket (this)
            _rigidbody.velocity = Vector3.zero; //reset movement velocity
            _rigidbody.angularVelocity = Vector3.zero; //reset rotation velocity
            _rigidbody.useGravity = false; //disable gravity 
            _trackRotation = false; 

            this.gameObject.SetActive(false);
        }
    }
}
