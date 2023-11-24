using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static int highscore;
    private static string currentTeam;
    private static int currentScore;
    private static int gameSessionTimer;
    private static int enemySpawnTimer;

    private static ShipStruct enemyCurrentTeam;

    private static DataManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.SetString("CurrentTeam", currentTeam);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.SetInt("GameSessionTimer", gameSessionTimer);
        PlayerPrefs.SetInt("EnemySpawnTimer", enemySpawnTimer);

        PlayerPrefs.Save();
    }
    private void LoadData()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        currentTeam = PlayerPrefs.GetString("CurrentTeam", "");
        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        gameSessionTimer = PlayerPrefs.GetInt("GameSessionTimer", 0);
        enemySpawnTimer = PlayerPrefs.GetInt("EnemySpawnTimer", 0);
    }

    public static void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    #region Get/Set Score
    public static int GetHighscore()
    {
        return highscore;
    }

    public static void SetHighscore(int score)
    {
        highscore = score;
    }
    public static int GetScore()
    {
        return currentScore;
    }
    public static void AddCurrentScore(int score)
    {
        currentScore += score;
    }
    public static void SetScore(int score)
    {
        currentScore = score;
    }
    #endregion

    #region Get/Set Current Teams
    public static string GetPlayerCurrentTeam()
    {
        return currentTeam;
    }

    public static void SetPlayerCurrentTeam(string team)
    {
        currentTeam = team;
    }

    public static ShipStruct GetEnemyCurrentTeam()
    {
        return enemyCurrentTeam;
    }

    public static void SetEnemyCurrentTeam(ShipStruct team)
    {
        enemyCurrentTeam = team;
    }
    #endregion

    #region Get/Set Game Settings
    public static int GetGameSessionTimer()
    {
        return gameSessionTimer;
    }

    public static void SetGameSessionTimer(int timer)
    {
        gameSessionTimer = timer;
    }
    public static int GetEnemySpawnTimer()
    {
        return enemySpawnTimer;
    }

    public static void SetEnemySpawnTimer(int timer)
    {
        enemySpawnTimer = timer;
    }

    #endregion
}