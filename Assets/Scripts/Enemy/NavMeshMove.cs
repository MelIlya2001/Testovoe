using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private NavMeshAgent _navMeshAgent;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_target.position);
    }

    private void Update()
    {
        SetDestination();
        RotateOnTarget();
    }

    private void SetDestination()
    {
        _navMeshAgent.SetDestination(_target.position);
    }

    private void RotateOnTarget()
    {
        Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
