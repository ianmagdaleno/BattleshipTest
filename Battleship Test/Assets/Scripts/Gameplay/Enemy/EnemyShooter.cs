using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [Space(10), Header("Specific Attributes")]

    [SerializeField] private Weapon weapon;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float RangeToAttackInMovement;

    private Transform playerTarget;
    protected override void Start()
    {
        base.Start();
        playerTarget = GetPlayer();
        weapon.Initialize(timeBetweenShoots);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (playerTarget != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer <= RangeToAttackInMovement)
            {
                ShipAction();
            }
        }
    }
    protected override void ShipAction()
    {
        base.ShipAction();
        weapon.Shoot(damage);
    }
}
