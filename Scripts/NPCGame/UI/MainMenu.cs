using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameObject startButton;
    GameObject settingsButton;
    GameObject instrButton;
    GameObject exitButton;
    GameObject header;

    void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("Start");
        settingsButton = GameObject.FindGameObjectWithTag("Settings");
        instrButton = GameObject.FindGameObjectWithTag("Instructions");
        exitButton = GameObject.FindGameObjectWithTag("Exit");
        header = GameObject.FindGameObjectWithTag("Header");
    }

    void Update()
    {
        Placement.DeadCenter(instrButton);
        Placement.Below(settingsButton, instrButton);
        Placement.Below(exitButton, settingsButton);
        Placement.Above(startButton, instrButton);
        Placement.TopCenter(header);
    }

    public void toGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void toInstructions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }
    public void toSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    public void exit()
    {
        Application.Quit();
    }

}
