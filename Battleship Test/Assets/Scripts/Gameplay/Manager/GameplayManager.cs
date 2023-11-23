using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button buttonStart;
    [SerializeField] private TMP_Text textSessionTimer;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private ChangeTeamUI changeTeamObject;

    private bool gameplayOn;
    private float currentTime;

    private PlayerComponentsManager playerComponentsManager;
    private GameObject currentPlayerInGame;

    void Start()
    {
        panelGameOver.SetActive(false);

        buttonStart.onClick.RemoveAllListeners();
        buttonStart.onClick.AddListener(() => Initialize());
    }

    public void Initialize()
    {
        panelTutorial.SetActive(false);
        currentTime = 60f; //tem que ser substituido
        gameplayOn = true;

        currentPlayerInGame =  Instantiate(playerPrefab);
        playerComponentsManager = currentPlayerInGame.GetComponent<PlayerComponentsManager>();
        playerComponentsManager.ChangeTeamSail(changeTeamObject.GetTeamChoice());
    }

    void Update()
    {
        if(gameplayOn) 
        { 
            currentTime -= Time.deltaTime;
            textSessionTimer.text = $"{currentTime.ToString("F1")} '' ";

            if(currentTime <= 0)
            {
                GameOver();
            }
        }
    }
    public void GameOver()
    {
        gameplayOn = false;
        Destroy(currentPlayerInGame);
        panelGameOver.SetActive(true);
    }
}
