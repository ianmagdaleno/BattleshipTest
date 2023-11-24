using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform[] turrentBarrels;
    [SerializeField] private Collider2D[] ownColliders;

    [SerializeField] private float fireRate;
    [SerializeField] private int poolCount = 15;
    [SerializeField] private GameObject bulletPrefab;

    private float delayToNextShoot;
    private bool canShoot = true;
    private ObjectPool bulletPool;

    private void Awake()
    {
        bulletPool = GetComponent<ObjectPool>();
    }
    private void Start()
    {
        bulletPool.Initialize(bulletPrefab, poolCount);
    }

    public void Shoot()
    {
        if(canShoot)
        {
            //canShoot = false;
            //delayToNextShoot = fireRate;

            foreach(var currentBarrel in turrentBarrels)
            {
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = currentBarrel.position;
                bullet.transform.localRotation = currentBarrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();

                foreach (var collider in ownColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}
