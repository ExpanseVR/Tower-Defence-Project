using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    [SerializeField] 
    Transform _target;

    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _agent.SetDestination(_target.position);
        }
    }
}
