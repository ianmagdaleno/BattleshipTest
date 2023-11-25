using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Attributes")]
    
    [Range(.5f, 2f)]
    [SerializeField] private float lifeTime = 2f;
    
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosion;

    private float damage = 10f;
    private Rigidbody2D localRigidbody;

    private void Awake()
    {
        localRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Initialize(float newDamage = 0f)
    {
        if (newDamage > 0)
        {
            damage = newDamage;
        }

        localRigidbody.velocity = transform.right * speed;
        StartCoroutine(DisableAfterDelay());
    }
    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject currentExplosion = Instantiate(explosion,transform.position, transform.rotation);
        if (collision.GetComponent<HealthShipManager>() != null)
        {
            collision.GetComponent<HealthShipManager>().TakeDamage(damage);
        }
        Destroy(currentExplosion, 0.3f);
        this.gameObject.SetActive(false);
    }
}