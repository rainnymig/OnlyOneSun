using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject otherPanel;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
        if (otherPanel != null)
        {
            otherPanel.SetActive(false);
        }
    }

    public void StartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
