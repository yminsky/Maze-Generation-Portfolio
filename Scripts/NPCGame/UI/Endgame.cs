using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    GameObject youWonText;
    GameObject youLostText;
    GameObject restartButton;
    GameObject mainMenuButton;
    GameObject exitButton;

    void Start()
    {
        youWonText = GameObject.FindGameObjectWithTag("YouWon");
        youLostText = GameObject.FindGameObjectWithTag("YouLost");
        restartButton = GameObject.FindGameObjectWithTag("Start");
        mainMenuButton = GameObject.FindGameObjectWithTag("MainMenu");
        exitButton = GameObject.FindGameObjectWithTag("Exit");
        if (GameManager.gameWon)
        {
            youLostText.gameObject.SetActive(false);
        }
        else youWonText.gameObject.SetActive(false);
    }

    void Update()
    {
        Placement.TopCenter(youWonText);
        Placement.TopCenter(youLostText);
        Placement.DeadCenter(restartButton);
        Placement.Below(mainMenuButton, restartButton);
        Placement.Below(exitButton, mainMenuButton);
    }

    public void toGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void toMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void exit()
    {
        Application.Quit();
    }
}
