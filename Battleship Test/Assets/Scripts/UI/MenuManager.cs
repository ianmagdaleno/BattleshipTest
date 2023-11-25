using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> allPanels;
    [SerializeField] private ScreenLoaderFade fadeManager;

    [Header("Main menu")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;

    [Space(10), Header("Settings menu")]

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonQuitGame;
    [SerializeField] private Slider sliderSessionTimer;
    [SerializeField] private Slider sliderSpawnTimer;

    [SerializeField] private string textToSliderGameTimer;
    [SerializeField] private string textToSliderSpawnEnemyTimer;

    private DataManager dataManager;

    private void Start()
    {
        dataManager = FindAnyObjectByType<DataManager>();
        Initialize();
    }
    private void Initialize()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);

        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonBack.onClick.RemoveAllListeners();
        buttonQuitGame.onClick.RemoveAllListeners();

        buttonPlay.onClick.AddListener(() => LoadScene("Game"));
        buttonSettings.onClick.AddListener(() => LoadPanel(settingsPanel));
        buttonBack.onClick.AddListener(() => LoadPanel(mainPanel));
        buttonQuitGame.onClick.AddListener(() => QuitApplication());

        sliderSessionTimer.value = DataManager.GetGameSessionTimer();
        sliderSpawnTimer.value = DataManager.GetEnemySpawnTimer();

        sliderSessionTimer.maxValue = 180;
        sliderSessionTimer.minValue = 60;
        sliderSpawnTimer.maxValue = 10;
        sliderSpawnTimer.minValue = 1;

        UpdateText(sliderSessionTimer);
        UpdateText(sliderSpawnTimer);
        sliderSessionTimer.onValueChanged.AddListener(delegate { UpdateText(sliderSessionTimer);});
        sliderSpawnTimer.onValueChanged.AddListener(delegate { UpdateText(sliderSpawnTimer);});
    }

    public void LoadPanel(GameObject panel)
    {
        foreach(GameObject currentPanel in allPanels)
        {
            currentPanel.SetActive(false);
        }
        //fade in and fade out with delay
        panel.SetActive(true);
    }

    public void UpdateText(Slider sliderToUpdate)
    {
        if (sliderToUpdate == sliderSessionTimer)
        {
            DataManager.SetGameSessionTimer((int)sliderToUpdate.value);

            TMP_Text currentText = sliderToUpdate.transform.GetChild(0).GetComponent<TMP_Text>();
            if (currentText != null)
            {
                currentText.text = $"{textToSliderGameTimer} {sliderToUpdate.value.ToString("F1")} ''";
                dataManager.SaveData();
            }
        }
        else
        {
            DataManager.SetEnemySpawnTimer((int)sliderToUpdate.value);

            TMP_Text currentText = sliderToUpdate.transform.GetChild(0).GetComponent<TMP_Text>();
            if (currentText != null)
            {
                currentText.text = $"{textToSliderSpawnEnemyTimer} {sliderToUpdate.value.ToString("F1")} ''";
                dataManager.SaveData();
            }
        }
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        fadeManager.TransitionNextScreen(sceneName);
    }
}
