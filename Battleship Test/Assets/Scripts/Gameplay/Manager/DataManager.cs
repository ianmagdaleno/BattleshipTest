using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private static int highscore;
    [SerializeField] private static string currentTeam;
    [SerializeField] private static int currentScore;
    [SerializeField] private static int gameSessionTimer;
    [SerializeField] private static int enemySpawnRateTimer;

    [SerializeField] private static ShipStruct enemyCurrentTeam;

    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("DataManager");
                    _instance = singletonObject.AddComponent<DataManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
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

    public static int GetGameSessionTimer()
    {
        return gameSessionTimer;
    }

    public static void SetGameSessionTimer(int timer)
    {
        gameSessionTimer = timer;
    }
}