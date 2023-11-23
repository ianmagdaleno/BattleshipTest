using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private int speedMovement;

    [Range(1, 20)]
    [SerializeField] private int damage;

    [SerializeField] private float maxHealth;
    [SerializeField] private HealthShipManager healthShipManager;
    [SerializeField] private Transform targetPlayer;

    private void Start()
    {
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        healthShipManager.Initialize(DataManager.GetEnemyCurrentTeam());
    }

    public void FollowTheTarget()
    {
        if (targetPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speedMovement * Time.deltaTime);
        }
    }
}
