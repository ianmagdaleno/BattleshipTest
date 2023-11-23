using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> allPanels;

    [Header("Main menu")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;

    [Space(10), Header("Settings menu")]

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Slider sliderSessionTimer;
    [SerializeField] private Slider sliderSpawnTimer;


    [Range(60, 180)]
    static float sessionTimer;

    //static float 

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);

        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonBack.onClick.RemoveAllListeners();

        buttonPlay.onClick.AddListener(() => LoadScene("Game"));
        buttonSettings.onClick.AddListener(() => LoadPanel(settingsPanel));
        buttonBack.onClick.AddListener(() => LoadPanel(mainPanel));

        //set current values and max values
        //sliderSessionTimer.onValueChanged.AddListener(() => UpdateText(sliderSessionTimer));
        //sliderSpawnTimer.onValueChanged.AddListener(() => UpdateText(sliderSpawnTimer));
    }

    public void LoadPanel(GameObject panel)
    {
        foreach(GameObject currentPanel in allPanels)
        {
            currentPanel.SetActive(false);
        }
        panel.SetActive(true);
    }
    
    public void UpdateText(Slider sliderToUpdate)
    {
        //if(sliderToUpdate == sliderSessionTimer)
        //{

        //}
    } 

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
