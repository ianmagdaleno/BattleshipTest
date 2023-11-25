using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private ScreenLoaderFade fadeManager;

    [Space(10),Header("UI GameObjects")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject prefabRankItem;
    [SerializeField] Transform contentRank;

    [Space(10), Header("UI Buttons")]
    [SerializeField] private Button buttonPlayAgain;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonStart;

    [Space(10), Header("UI Texts")]
    [SerializeField] private TMP_Text textSessionTimer;
    [SerializeField] private TMP_Text textScoreInGame;
    [SerializeField] private TMP_Text textEndScore;
  
    [SerializeField] private string textLabelEndScore;
    [SerializeField] private string textLabelScoreInGame;

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
        UpdateRank();
    }
    public void UpdateRank()
    {
        if(contentRank.childCount > 0)
        {
            for (int i = contentRank.childCount - 1; i >= 0; i--)
            {
                GameObject child = contentRank.GetChild(i).gameObject;
                Destroy(child);
            }
        }
        foreach (int score in DataManager.HighScores)
        {
            TMP_Text newScore = Instantiate(prefabRankItem, contentRank).GetComponentInChildren<TMP_Text>();
            newScore.text = $"{score} Points";
        }
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
