using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTeamUI : MonoBehaviour
{
     private ShipStruct[] allSails;

    [SerializeField] private Image previewImageRenderer;

    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPrevious;

    private int indexCurrentSail;

    private void Start()
    {
        buttonNext.onClick.RemoveAllListeners();
        buttonPrevious.onClick.RemoveAllListeners();

        buttonNext.onClick.AddListener(() => Next());
        buttonPrevious.onClick.AddListener(() => Previous());

        allSails = Resources.LoadAll<ShipStruct>("ScriptableObjects/Sail");
        indexCurrentSail = 0;
        previewImageRenderer.sprite = allSails[indexCurrentSail].PreviewImage;
    }
    public void Next()
    {
        if(indexCurrentSail < allSails.Length -1)
        {
            indexCurrentSail++;
        }
        else
        {
            indexCurrentSail = 0;
        }
       
        previewImageRenderer.sprite = allSails[indexCurrentSail].PreviewImage;
    }
    public void Previous()
    {
        if (indexCurrentSail > 0)
        {
            indexCurrentSail--;
        }
        else
        {
            indexCurrentSail = allSails.Length - 1;
        }
        previewImageRenderer.sprite = allSails[indexCurrentSail].PreviewImage;
    }

    public ShipStruct GetTeamChoice()
    {
       return allSails[indexCurrentSail];
    }
}
