using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField]
    Transform _target;

    private void OnEnable()
    {
        GetComponent<NavMeshAgent>().SetDestination(_target.position);
    }
}
