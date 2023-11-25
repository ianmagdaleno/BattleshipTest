using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBasic : MonoBehaviour
{
    [SerializeField] private float timeToSlowdownEffect;
    [SerializeField] private float durationSlowdownEffect;
    [SerializeField] private float differenceSpeedEffect;
    [SerializeField] private EffectBasic tagComponentEffect;

    private bool effectActive;
    private SpriteRenderer[] currentShipAppearance;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthShipManager>())
        {
            effectActive = true;

            if (collision.gameObject.CompareTag("Player"))
            {
                TagComponentEffect(collision.gameObject);

                PlayerController shipBody = collision.gameObject.GetComponent<PlayerController>();
                currentShipAppearance = shipBody.GetShipAppearence();
                StartCoroutine(ObstacleVisualEffect(currentShipAppearance));
            }
        }
    }
    private void TagComponentEffect(GameObject targetObject)
    {
        EffectBasic existingComponent = targetObject.GetComponent<EffectBasic>();

        if (existingComponent != null && existingComponent.GetType() != tagComponentEffect.GetType())
        {
            Destroy(existingComponent);
        }
        if (existingComponent == null || existingComponent.GetType() != tagComponentEffect.GetType())
        {
            targetObject.AddComponent(tagComponentEffect.GetType());
            targetObject.GetComponent<EffectBasic>().InitializeSlowdown(durationSlowdownEffect,differenceSpeedEffect);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthShipManager>())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                effectActive = false;
                StopCoroutine(ObstacleVisualEffect(currentShipAppearance));
                ResetShipAlpha(currentShipAppearance);
            }
        }
    }
    private IEnumerator ObstacleVisualEffect(SpriteRenderer[] shipAppearance)
    {
        while (effectActive)
        {
            ToggleShipAlpha(shipAppearance);
            yield return new WaitForSeconds(0.2f); 
        }
        yield return new WaitForSeconds(timeToSlowdownEffect);

        ResetShipAlpha(shipAppearance);
    }
    private void ToggleShipAlpha(SpriteRenderer[] shipAppearance)
    {
        if(shipAppearance != null)
        {
            foreach (SpriteRenderer spriteRenderer in shipAppearance)
            {
                if (spriteRenderer)
                {
                    float newAlpha = (spriteRenderer.color.a == 0.0f) ? 1.0f : 0.0f;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
                }
            }
        }
    }
    private void ResetShipAlpha(SpriteRenderer[] shipAppearance)
    {
        foreach (SpriteRenderer spriteRenderer in shipAppearance)
        {
            if (spriteRenderer)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
            }
        }
    }
}