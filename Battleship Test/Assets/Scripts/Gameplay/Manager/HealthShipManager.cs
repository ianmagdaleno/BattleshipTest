using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthShipManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;

    [Header("Ship stats")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [Space(10),Header("Ship state objects")]
    [SerializeField] private SpriteRenderer shipBodySprite;
    [SerializeField] private SpriteRenderer shipSailSprite;
    [SerializeField] private ShipStruct shipBodyPhases;
    [SerializeField] private ShipStruct shipSailPhases;

    [Space(10), Header("Destroy Animations")]
    [SerializeField] private Transform[] destroyAnimationPosition;
    [SerializeField] private GameObject fireAnimation;
    [SerializeField] private GameObject bigExplosionAnimation;
    

    public void Initialize(ShipStruct newShipSail)
    {
        shipSailPhases = newShipSail;
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        UpdateStats();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth > 0)
        {

            UpdateStats();
        }
        else
        {
            DestroyAnimation(bigExplosionAnimation);

            if (this.gameObject.CompareTag("Player"))
            {
                gameplayManager.GameOver();
            }
            else
            {
                DataManager.AddCurrentScore(1);
                Destroy(transform.parent.gameObject,0.4f);
            }
        }
    }
    public void UpdateStats()
    {
        healthSlider.value = currentHealth;

        float healthPercentage = currentHealth / maxHealth;

        if (healthPercentage >= 0.75f)
        {
            shipBodySprite.sprite = shipBodyPhases.shipPart[0];
            shipSailSprite.sprite = shipSailPhases.shipPart[0];
        }
        else if (healthPercentage >= 0.5f)
        {
            if (shipBodySprite.sprite != shipBodyPhases.shipPart[1])
            {
                DestroyAnimation(fireAnimation);
                shipBodySprite.sprite = shipBodyPhases.shipPart[1];
                shipSailSprite.sprite = shipSailPhases.shipPart[1];
            }
        }
        else if (healthPercentage >= 0.25f)
        {
            if (shipBodySprite.sprite != shipBodyPhases.shipPart[2])
            {
                DestroyAnimation(fireAnimation);
                shipBodySprite.sprite = shipBodyPhases.shipPart[2];
                shipSailSprite.sprite = shipSailPhases.shipPart[2];
            }
        }
        else
        {
            if (shipBodySprite.sprite != shipBodyPhases.shipPart[3])
            {
                DestroyAnimation(fireAnimation);
                shipBodySprite.sprite = shipBodyPhases.shipPart[3];
                shipSailSprite.sprite = shipSailPhases.shipPart[3];
            }
        }
    }
    public void DestroyAnimation(GameObject animation)
    {
        foreach(Transform currentPosition in destroyAnimationPosition)
        {
            GameObject currentAnimation = Instantiate(animation, currentPosition);
            Destroy(currentAnimation, 1f);
        }
    }
}
