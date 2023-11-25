using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy basic Attributes")]
    [Range(1, 20)]
    [SerializeField] private float speed;

    [Range(1, 20)]
    [SerializeField] protected int damage;

    [Range(0.3f, 5f)]
    [SerializeField] private float rangeToAct;

    [Space(10), Header("Enemy basic Components")]
    [SerializeField] private HealthShipManager healthShipManager;
    [SerializeField] private Rigidbody2D localRigidbody;
    [SerializeField] Transform bodyTransform;

    private Transform targetPlayer;
    private Pathfinding pathfinding;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    protected virtual void Start()
    {
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            healthShipManager.Initialize(DataManager.GetEnemyCurrentTeam());
        }

        pathfinding = new Pathfinding(20, 12);
    }

    protected virtual void FixedUpdate()
    {
        if(targetPlayer != null)
        {
            RotateAim();
            HandleMovement();
            SetTargetPosition(targetPlayer.position);
            DrawPathToTarget(targetPlayer.position);
        }
    }

    protected virtual void RotateAim()
    {
        if (targetPlayer != null)
        {
            Vector2 moveDirection = (targetPlayer.position - transform.position).normalized;
            float aimAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            localRigidbody.rotation = aimAngle;
        }
    }
    protected virtual void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > rangeToAct)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDirection * speed * Time.deltaTime;
            }
            else
            {
                ShipAction();
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }
    protected Transform GetPlayer()
    {
        return targetPlayer;
    }
    protected virtual void ShipAction()
    {
        
    }
    protected virtual void StopMoving()
    {
        pathVectorList = null;
    }
    protected void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = pathfinding.FindPath(transform.position, targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
    protected void DrawPathToTarget(Vector3 targetPosition)
    {
        pathfinding.GetGrid().GetXY(transform.position, out int startX, out int startY);
        pathfinding.GetGrid().GetXY(targetPosition, out int endX, out int endY);

        List<PathNode> path = pathfinding.FindPath(startX, startY, endX, endY);

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) + Vector3.one * .5f, new Vector3(path[i + 1].x, path[i + 1].y) + Vector3.one * .5f, Color.red);
            }
        }
    }
}