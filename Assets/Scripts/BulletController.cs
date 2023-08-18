using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float damage = 50.0F;

    [SerializeField]
    LayerMask whatIsEnemy;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == (other.gameObject.layer | (1 << whatIsEnemy)))
        {
            HealthController controller = other.gameObject.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthController controller = other.GetComponent<HealthController>();
            controller.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
