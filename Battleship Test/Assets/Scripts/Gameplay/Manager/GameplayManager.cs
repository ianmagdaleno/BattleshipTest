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

    [SerializeField] private SpawnEnemies spawnerEnemies; 

    private bool gameplayOn;
    private float currentSessionGameTimer;

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
        currentSessionGameTimer = DataManager.GetGameSessionTimer();
        gameplayOn = true;
        spawnerEnemies.Initialize();

        currentPlayerInGame =  Instantiate(playerPrefab);
        playerComponentsManager = currentPlayerInGame.GetComponent<PlayerComponentsManager>();
        playerComponentsManager.ChangeTeamSail(changeTeamObject.GetTeamChoice());
    }

    void Update()
    {
        if(gameplayOn) 
        { 
            currentSessionGameTimer -= Time.deltaTime;
            textSessionTimer.text = $"{currentSessionGameTimer.ToString("F1")} '' ";

            if(currentSessionGameTimer <= 0)
            {
                GameOver();
            }
        }
    }
    public void GameOver()
    {
        gameplayOn = false;
        spawnerEnemies.gameplayOn = false;
        Destroy(currentPlayerInGame);
        panelGameOver.SetActive(true);
    }
}
