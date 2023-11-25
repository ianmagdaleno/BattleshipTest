using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHUDManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;

    [Header("UI Components")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private Button buttonStart;
    [SerializeField] private TMP_Text textSessionTimer;

    [SerializeField] TMP_Text textScoreInGame;
    [SerializeField] string textLabelScoreInGame;

    [SerializeField] TMP_Text textEndScore;
    [SerializeField] string textLabelEndScore;

    [SerializeField] Button buttonPlayAgain;
    [SerializeField] Button buttonMainMenu;

    private bool gameOn;
    private float currentGameTimeToEnd;
    private void Start()
    {
        panelGameOver.SetActive(false);
        buttonStart.onClick.RemoveAllListeners();
        buttonPlayAgain.onClick.RemoveAllListeners();
        buttonMainMenu.onClick.RemoveAllListeners();

        buttonStart.onClick.AddListener(() => gameplayManager.Initialize());
        buttonStart.onClick.AddListener(() => panelTutorial.SetActive(false));
        buttonPlayAgain.onClick.AddListener(() => LoadScene("Game"));
        buttonMainMenu.onClick.AddListener(() => LoadScene("MainMenu"));
    }
    public void Initialize()
    {
        gameOn = true;
        currentGameTimeToEnd = DataManager.GetGameSessionTimer();
    }
    public void Update()
    {
        if (gameOn)
        {
            currentGameTimeToEnd -= Time.deltaTime;
            textSessionTimer.text = $"{currentGameTimeToEnd.ToString("F1")} '' ";

            if (currentGameTimeToEnd <= 0)
            {
                gameplayManager.GameOver();
                GameOverScreen();
            }
        }
    }
    private void GameOverScreen()
    {
        panelGameOver.SetActive(true);
    }
    private void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void UpdateScore(int newScore)
    {
        if (gameplayManager.GetGameStatus())
        {
            textScoreInGame.text = $"{textLabelScoreInGame} {newScore}";
        }
        else
        {
            textEndScore.text = $"{textLabelEndScore} {newScore}";
        }
    }
    public void UpdateGameState(bool gameStatus)
    {
        gameOn = gameStatus;
        if (!gameOn)
        {
            GameOverScreen();
        }
    }
}