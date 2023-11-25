using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private ScreenLoaderFade fadeManager;

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
        buttonPlayAgain.onClick.AddListener(() => fadeManager.TransitionNextScreen("Game"));
        buttonMainMenu.onClick.AddListener(() => fadeManager.TransitionNextScreen("MainMenu"));
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
        UpdateScore();
    }
    public void UpdateScore()
    {
        if (gameplayManager.GetGameStatus())
        {
            textScoreInGame.text = $"{textLabelScoreInGame} {DataManager.GetScore()}";
        }
        else
        {
            textEndScore.text = $"{textLabelEndScore} {DataManager.GetScore()}";
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
