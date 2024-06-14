using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPlayButtonClicked(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnControlsButtonClicked()
    {
        controlsPanel.SetActive(true);
    }

    public void OnXButtonClicked()
    {
        controlsPanel.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnCreditButtonClick()
    {
        creditsScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
