using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    enum PatrolStates
    { 
        Patrol,
        Chase,
        Attack
    }

    [SerializeField]
    Transform target;

    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    float sightRange;

    [SerializeField]
    float attackRange;

    [SerializeField]
    float walkRange;

    [SerializeField]
    float attackTimeOut;

    NavMeshAgent _navAgent;

    PatrolStates _currentState;

    LayerMask _whatIsTarget;
    Vector3 _walkPoint;

    bool _isTargetInSightRange;
    bool _isTargetInAttackRange;
    bool _hasWalkPoint;
    bool _isAttacking;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkRange);
    }

    void Awake()
    {
        _whatIsTarget = transform.gameObject.layer;
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _isTargetInSightRange =
            Physics.CheckSphere(transform.position, sightRange, _whatIsTarget);

        _isTargetInAttackRange =
            Physics.CheckSphere(transform.position, attackRange, _whatIsTarget);

        if (_isTargetInAttackRange )
        {
            HandleAttack();
        }
        else if (_isTargetInSightRange)
        {
            HandleCase();
        }
        else
        {
            HanldePatrol();
        }
    }

    void HanldePatrol()
    {
        if (!_hasWalkPoint)
        {
            float positionX = Random.Range(-walkRange, walkRange);
            float positionZ = Random.Range(-walkRange, walkRange);

            _walkPoint = transform.position;
            _walkPoint.x = positionX;
            _walkPoint.z = positionZ;

            _hasWalkPoint = Physics.Raycast(_walkPoint, -transform.up, whatIsGround);
            if (_hasWalkPoint ) 
            {
                _navAgent.SetDestination(_walkPoint);
            }
        }
        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if(distanceToWalkPoint.magnitude < 1.0F)
        {
            _hasWalkPoint = false;
        }
    }

    void HandleCase()
    {
        _navAgent.SetDestination(target.position);
    }

    void HandleAttack()
    {
        _navAgent.SetDestination(transform.position);
        transform.LookAt(target.position);

        if (!_isAttacking)
        {
            _isAttacking = true;
            Invoke(nameof(ResetAttack), attackTimeOut);
        }
    }

    void ResetAttack()
    {
        _isAttacking = false;
    }
}
