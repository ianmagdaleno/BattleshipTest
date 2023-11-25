using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaster : Enemy
{
    [Space(10), Header("Specific Attributes")]

    [SerializeField] private float minDistanceToExplode;
    [SerializeField] private GameObject bigExplosionAnimation;
    
    private Transform playerTarget;
    protected override void Start()
    {
        base.Start();
        playerTarget = GetPlayer();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (playerTarget != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer <= minDistanceToExplode)
            {
                ShipAction();
            }
        }
    }
    protected override void ShipAction()
    {
        base.ShipAction();
        StartCoroutine(selfExplosion());
    }
    IEnumerator selfExplosion()
    {
        HealthShipManager playerHealth = playerTarget.gameObject.GetComponent<HealthShipManager>();
        Instantiate(bigExplosionAnimation, transform);

        yield return new WaitForSeconds(0.3f);

        playerHealth.TakeDamage(damage);
        Destroy(this.transform.parent.gameObject);
    }
}
