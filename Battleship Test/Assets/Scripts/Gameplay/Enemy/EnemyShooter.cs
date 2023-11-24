using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private float rangeToShoot;
    [SerializeField] private Weapon weapon;

    private float distanceAtPlayer;
    private void  FixedUpdate()
    {
        //if(distanceAtPlayer > rangeToShoot)
        //{
            FollowTheTarget();
        //}
        //else 
        //{
        //    weapon.Shoot();
        //}
    }
}
