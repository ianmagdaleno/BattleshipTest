using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes and Components")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [Range(5,20)]
    [SerializeField] private float damageSingleShoot;
    [Range(5, 20)]
    [SerializeField] private float damageTripleShoot;
    [SerializeField] private Rigidbody2D localRigidbody;
    [SerializeField] private SpriteRenderer[] allShipComponents;

    [Header("Weapons Attributes and Components")]
    [SerializeField] private float timeBetweenSingleShoot;
    [SerializeField] private float timeBetweenTripleShoot;
    [SerializeField] private Weapon singleWeapon;
    [SerializeField] private Weapon multiWeaponLeft;
    [SerializeField] private Weapon multiWeaponRight;

    private void Start()
    {
        singleWeapon.Initialize(timeBetweenSingleShoot);
        multiWeaponLeft.Initialize(timeBetweenTripleShoot);
        multiWeaponRight.Initialize(timeBetweenTripleShoot);
    }
    private void Update()
    {
        MovePlayer();
        RotatePlayer();

        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Space))
        {
            singleWeapon.Shoot(damageSingleShoot);
        }
        if (Input.GetMouseButtonDown(0))
        {
            multiWeaponLeft.Shoot(damageTripleShoot);
        }
        if (Input.GetMouseButtonDown(1))
        {
            multiWeaponRight.Shoot(damageTripleShoot);
        }
    }
    private void MovePlayer()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        if(moveY > 0)
        {
            Vector2 moveDirection = transform.up * moveY;
            localRigidbody.velocity = moveDirection * moveSpeed;
        }
        else
        {
            localRigidbody.velocity = Vector2.zero;
        }
    }
    private void RotatePlayer()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - localRigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        localRigidbody.rotation = aimAngle;
    }
    public SpriteRenderer[] GetShipAppearence()
    {
        return allShipComponents;
    }
    public void ChangeShipSpeed(float newSpeed)
    {
        moveSpeed += newSpeed;
    }
}

