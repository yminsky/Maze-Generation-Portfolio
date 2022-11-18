using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    GameObject startButton;
    GameObject mainMenuButton;
    GameObject instructions;
    GameObject header;

    void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("Start");
        mainMenuButton = GameObject.FindGameObjectWithTag("MainMenu");
        instructions = GameObject.FindGameObjectWithTag("Instructions");
        header = GameObject.FindGameObjectWithTag("Header");
    }

    void Update()
    {
        Placement.TopCenter(header);
        Placement.Below(instructions, header);
        Placement.BottomLeft(startButton);
        Placement.BottomRight(mainMenuButton);
    }

    public void toGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void toMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}


