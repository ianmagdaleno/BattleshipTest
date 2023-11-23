using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthShipManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("ship state")]
    [SerializeField] private SpriteRenderer shipBodySprite;
    [SerializeField] private SpriteRenderer shipSailSprite;

    [SerializeField] private ShipStruct shipBodyPhases;
    [SerializeField] private ShipStruct shipSailPhases;

    private void Start()
    {
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
            Debug.Log("Game Over");
            Destroy(transform.parent.gameObject);
            //give points
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
            shipBodySprite.sprite = shipBodyPhases.shipPart[1];
            shipSailSprite.sprite = shipSailPhases.shipPart[1];
        }
        else if (healthPercentage >= 0.25f)
        {
            shipBodySprite.sprite = shipBodyPhases.shipPart[2];
            shipSailSprite.sprite = shipSailPhases.shipPart[2];
        }
        else
        {
            shipBodySprite.sprite = shipBodyPhases.shipPart[3];
            shipSailSprite.sprite = shipSailPhases.shipPart[3];
        }
    }
}
