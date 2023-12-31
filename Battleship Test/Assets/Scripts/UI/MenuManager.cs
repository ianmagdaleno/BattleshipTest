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
    [SerializeField] private Button buttonSound;
    [SerializeField] private Slider sliderSessionTimer;
    [SerializeField] private Slider sliderSpawnTimer;

    [SerializeField] private string textToSliderGameTimer;
    [SerializeField] private string textToSliderSpawnEnemyTimer;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private Sprite[] buttonSoundVariant;

    private DataManager dataManager;
    private bool soundEnabled = true;

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
        buttonSound.onClick.RemoveAllListeners();

        buttonPlay.onClick.AddListener(() => LoadScene("Game"));
        buttonSettings.onClick.AddListener(() => LoadPanel(settingsPanel));
        buttonBack.onClick.AddListener(() => LoadPanel(mainPanel));
        buttonQuitGame.onClick.AddListener(() => QuitApplication());
        buttonSound.onClick.AddListener(() => ChangeSoundState());

        buttonPlay.onClick.AddListener(() => AudioSource.Instantiate(clickSound));
        buttonSettings.onClick.AddListener(() => AudioSource.Instantiate(clickSound));
        buttonBack.onClick.AddListener(() => AudioSource.Instantiate(clickSound));
        buttonQuitGame.onClick.AddListener(() => AudioSource.Instantiate(clickSound));

        sliderSessionTimer.value = DataManager.GetGameSessionTimer();
        sliderSpawnTimer.value = DataManager.GetEnemySpawnTimer();

        sliderSessionTimer.maxValue = 180;
        sliderSessionTimer.minValue = 60;
        sliderSpawnTimer.maxValue = 10;
        sliderSpawnTimer.minValue = 1;

        LoadDataSlider(sliderSessionTimer);
        LoadDataSlider(sliderSpawnTimer);
        sliderSessionTimer.onValueChanged.AddListener(delegate { UpdateText(sliderSessionTimer);});
        sliderSpawnTimer.onValueChanged.AddListener(delegate { UpdateText(sliderSpawnTimer);});
    }
    public void ChangeSoundState()
    {
        soundEnabled = !soundEnabled;
        AudioListener.volume = soundEnabled ? 1 : 0;
        if (soundEnabled) 
            buttonSound.image.sprite = buttonSoundVariant[0];
        else
            buttonSound.image.sprite = buttonSoundVariant[1];
    }
    public void LoadPanel(GameObject panel)
    {
        foreach(GameObject currentPanel in allPanels)
        {
            currentPanel.SetActive(false);
        }
        panel.SetActive(true);
    }
    public void LoadDataSlider(Slider sliderToUpdate)
    {
        if (sliderToUpdate == sliderSessionTimer)
        {
            sliderToUpdate.value = DataManager.GetGameSessionTimer();
        }
        else
        {
            sliderToUpdate.value = DataManager.GetEnemySpawnTimer();
        }

        UpdateText(sliderToUpdate);
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
