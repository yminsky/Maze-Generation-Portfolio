using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour
{
    GameObject mainMenu;

    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
    }

    void Update()
    {
        Placement.BottomRight(mainMenu);
    }

    public void toMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
