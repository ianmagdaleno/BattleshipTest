using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowShip : MonoBehaviour
{
    [SerializeField] private Transform shipToFollow;
    [SerializeField] private Vector3 offSet;
    
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(shipToFollow != null)
        {
            rectTransform.anchoredPosition = shipToFollow.localPosition + offSet;
        }
    }
}
