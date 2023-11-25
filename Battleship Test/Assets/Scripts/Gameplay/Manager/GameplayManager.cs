using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private ChangeTeamUI changeTeamObject;
    [SerializeField] private GameHUDManager uiManager;

    [Space(10), Header("In game objects")]
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private SpawnEnemies spawnerEnemies; 

    private bool gameplayOn;
    private ShipTeamManager playerComponentsManager;
    private GameObject currentPlayerInGame;
    public void Initialize()
    {
        gameplayOn = true;
        uiManager.Initialize();
        currentPlayerInGame =  Instantiate(playerPrefab, playerSpawnPosition);
        playerComponentsManager = currentPlayerInGame.GetComponent<ShipTeamManager>();
        playerComponentsManager.ChangeTeamSail(changeTeamObject.GetTeamChoice());
        spawnerEnemies.Initialize();
    }
    public void PlayerScoreAdd()
    {
        DataManager.AddCurrentScore(1);
        uiManager.UpdateScore();
    }
    public void GameOver()
    {
        gameplayOn = false;
        spawnerEnemies.gameplayOn = false;
        Destroy(currentPlayerInGame, 0.4f);
        DataManager.UpdateHighScore(DataManager.GetScore());
        uiManager.UpdateGameState(gameplayOn);
    }
    public bool GetGameStatus()
    {
        return gameplayOn;
    }
}
