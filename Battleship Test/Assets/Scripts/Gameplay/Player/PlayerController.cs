using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private Rigidbody2D localRigidbody;

    private void Update()
    {
        MovePlayer();
        RotatePlayer();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack !!!!");
        }
    }

    private void MovePlayer()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = transform.up * moveY;
        localRigidbody.velocity = moveDirection * moveSpeed;
    }

    private void RotatePlayer()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - localRigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        localRigidbody.rotation = aimAngle;
    }
}

