﻿using UnityEngine;
using UnityEngine.AI;
using GameDevHQ.Scripts.Managers;


namespace GameDevHQ.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _health;

        [SerializeField]
        private int _warfund;

        private Transform _target;
        private NavMeshAgent _agent;

        private void OnEnable()
        {
            //get target
            _target = GameManger.Instance.RequestTarget();

            //move to target
            _agent = GetComponent<NavMeshAgent>();
            if (_agent != null)
            {
                _agent.SetDestination(_target.position);
                _agent.speed = _speed;
            }
        }
    }
}