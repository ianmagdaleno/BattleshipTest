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
        selfExplosion();
    }
    public void selfExplosion()
    {
        HealthShipManager playerHealth = playerTarget.gameObject.GetComponent<HealthShipManager>();
        playerHealth.TakeDamage(damage);
        GameObject explosionAnimation = Instantiate(bigExplosionAnimation, transform);
        Destroy(this.transform.parent.gameObject);
    }
}
