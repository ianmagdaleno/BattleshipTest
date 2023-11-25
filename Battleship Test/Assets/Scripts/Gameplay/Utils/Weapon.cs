using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Weapon : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [SerializeField] private Transform[] turrentBarrels;
    [SerializeField] private Collider2D[] ownColliders;
    [SerializeField] private int poolCount = 15;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;

    private float delayBetweenShoot;
    private bool canShoot;
    private ObjectPool bulletPool;

    public void Initialize(float fireRate)
    {
        bulletPool = GetComponent<ObjectPool>();

        bulletPool.Initialize(bulletPrefab, poolCount);
        canShoot = true;
        delayBetweenShoot = fireRate;
        this.fireRate = fireRate;
    }
    private void Update()
    {
        if (!canShoot)
        {
            delayBetweenShoot -= Time.deltaTime;

            if (delayBetweenShoot <= 0)
            {
                canShoot = true;
                delayBetweenShoot = fireRate;
            }
        }
    }
    public void Shoot(float damage)
    {
        if (canShoot)
        {
            foreach (var currentBarrel in turrentBarrels)
            {
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = currentBarrel.position;
                bullet.transform.localRotation = currentBarrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(damage);

                foreach (var collider in ownColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
            canShoot = false;
        }
    }
}