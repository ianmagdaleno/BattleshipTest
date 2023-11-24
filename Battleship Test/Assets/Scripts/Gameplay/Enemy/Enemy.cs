using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private float speed;

    [Range(1, 20)]
    [SerializeField] private int damage;

    [SerializeField] private float maxHealth;
    [SerializeField] private HealthShipManager healthShipManager;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private Rigidbody2D localRigidbody;
    [SerializeField] private LayerMask obstacleLayer;

    protected void Start()
    {
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            healthShipManager.Initialize(DataManager.GetEnemyCurrentTeam());
        }
    }

    protected void Update()
    {
        FollowTheTarget();
    }

    public void FollowTheTarget()
    {
        if (targetPlayer != null)
        {
            Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;

          
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 2f, obstacleLayer);
            //Debug.DrawRay(transform.position, transform.up * 2f, Color.green);

            //if (hit.collider != null)
            //{
            //    Debug.Log("Hit it");
            //    moveDirection = Quaternion.Euler(0, 0, Random.Range(90, 270)) * moveDirection;
            //}

            localRigidbody.velocity = moveDirection * speed;

            float aimAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            localRigidbody.rotation = aimAngle;
        }
    }
}