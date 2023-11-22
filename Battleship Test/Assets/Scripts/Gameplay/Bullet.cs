using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 2f; 

    private Rigidbody2D localRigidbody;

    private void Awake()
    {
        localRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Initialize()
    {
        localRigidbody.velocity = transform.right * speed;
        StartCoroutine(DisableAfterDelay());
    }
    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}