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
    [SerializeField] private AudioSource clickSound;

    private int indexCurrentSail;

    private void Start()
    {
        buttonNext.onClick.RemoveAllListeners();
        buttonPrevious.onClick.RemoveAllListeners();

        buttonNext.onClick.AddListener(() => Next());
        buttonPrevious.onClick.AddListener(() => Previous());
        buttonNext.onClick.AddListener(() => AudioSource.Instantiate(clickSound));
        buttonPrevious.onClick.AddListener(() => AudioSource.Instantiate(clickSound));
        

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
       SetRandomTimeToEnemy(indexCurrentSail);
       return allSails[indexCurrentSail];
    }
    public void SetRandomTimeToEnemy(int exeption)
    {
        int randomTeam = Random.Range(0, allSails.Length -1);
        
        if(randomTeam != exeption)
        {
            DataManager.SetEnemyCurrentTeam(allSails[randomTeam]);
        }
        else
        {
            SetRandomTimeToEnemy(exeption);
        }
    }
}
