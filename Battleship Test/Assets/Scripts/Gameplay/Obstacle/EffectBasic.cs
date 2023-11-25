using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBasic : MonoBehaviour
{
    [SerializeField] private float speedDifference;

    public void InitializeSlowdown(float duration, float speedDifference)
    {
        StartCoroutine(Slowdown(speedDifference, duration));
    }
    private IEnumerator Slowdown(float speedDifference, float duration)
    {
        GetComponent<PlayerController>().ChangeShipSpeed(-speedDifference);

        yield return new WaitForSeconds(duration);

        GetComponent<PlayerController>().ChangeShipSpeed(+speedDifference);
        Destroy(this);
    }
}
