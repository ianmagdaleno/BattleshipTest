using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaster : Enemy
{
    [Space(10), Header("Specific Attributes")]

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

            //if (distanceToPlayer <= RangeToAttackInMovement)
            //{
            //    ShipAction();
            //}
        }
    }
    protected override void ShipAction()
    {
        base.ShipAction();
        
    }
}
