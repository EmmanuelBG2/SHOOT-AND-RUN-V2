using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float attackRadius = 2.0F;

    [SerializeField]
    LayerMask whatIsTarget;

    [SerializeField]
    float damage = 50.0F;

    [SerializeField]
    int attackRate = 2;

    float _nextAttackTime = 0;

    List<Collider> _attackedColliders;

    void Awake()
    {
        _attackedColliders = new List<Collider>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextAttackTime)
        {
            _nextAttackTime = Time.time + 1.0F / attackRate;
            animator.SetTrigger("attack");

            _attackedColliders.Clear();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
        {
            OnAttack();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void OnAttack()
    {
        Collider[] colliders =
            Physics.OverlapSphere(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider collider in colliders)
        {
            if (_attackedColliders.Contains(collider))
            {
                continue;
            }
            HealthController controller = collider.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage);
                _attackedColliders.Add(collider);
            }
        }

        animator.ResetTrigger("attack");
    }


}
