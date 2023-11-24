using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [Space(10), Header("Specific Attributes")]

    [SerializeField] private Weapon weapon;
    [SerializeField] private float timeBetweenShoots;
    protected override void Start()
    {
        base.Start();
        weapon.Initialize(timeBetweenShoots);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void ShipAction()
    {
        base.ShipAction();
        weapon.Shoot();
    }
}
