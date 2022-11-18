using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    GameObject header;
    GameObject volumeHeader;
    GameObject highVolButton;
    GameObject lowVolButton;
    GameObject muteVolButton;
    GameObject mazeTypeHeader;
    GameObject backtrackerButton;
    GameObject sidewinderButton;
    GameObject binaryTreeButton;
    GameObject randomMazeButton;
    GameObject mainMenuButton;
    public static bool defaultMaze = true;
    public static bool defaultVol = true;

    void Start()
    {
        header = GameObject.FindGameObjectWithTag("Header");
        volumeHeader = GameObject.FindGameObjectWithTag("Volume");
        highVolButton = GameObject.FindGameObjectWithTag("HighVol");
        lowVolButton = GameObject.FindGameObjectWithTag("LowVol");
        muteVolButton = GameObject.FindGameObjectWithTag("MuteVol");
        mazeTypeHeader = GameObject.FindGameObjectWithTag("MazeType");
        backtrackerButton = GameObject.FindGameObjectWithTag("Backtracker");
        sidewinderButton = GameObject.FindGameObjectWithTag("Sidewinder");
        binaryTreeButton = GameObject.FindGameObjectWithTag("BinaryTree");
        randomMazeButton = GameObject.FindGameObjectWithTag("RandomMaze");
        mainMenuButton = GameObject.FindGameObjectWithTag("MainMenu");
    }

    void Update()
    {
        Placement.TopCenter(header);
        Placement.Below(mainMenuButton, header);
        Placement.Below(volumeHeader, mainMenuButton);
        volumeHeader.transform.position -= new Vector3(0, 10, 0);
        Placement.Below(lowVolButton, volumeHeader);
        lowVolButton.transform.position += new Vector3(0, 10, 0);
        Placement.toLeftOf(highVolButton, lowVolButton);
        Placement.toRightOf(muteVolButton, lowVolButton);
        Placement.Below(mazeTypeHeader, lowVolButton);
        mazeTypeHeader.transform.position -= new Vector3(0, 10, 0);
        Placement.Below(randomMazeButton, mazeTypeHeader);
        randomMazeButton.transform.position += new Vector3(0, 10, 0);
        Placement.Below(sidewinderButton, randomMazeButton);
        Placement.toLeftOf(backtrackerButton, sidewinderButton);
        Placement.toRightOf(binaryTreeButton, sidewinderButton);
        // Placement.Below(mainMenuButton, sidewinderButton);
    }

    public void toMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void highVol()
    {
        defaultVol = true;
    }

    public void lowVol()
    {
        GameManager.volume = 0.5f;
        defaultVol = false;
    }

    public void mute()
    {
        GameManager.volume = 0;
        defaultVol = false;
    }

    public void backtracker()
    {
        GameManager.mazeType = GameManager.MazeType.backtracker;
        defaultMaze = false;
    }

    public void sidewinder()
    {
        GameManager.mazeType = GameManager.MazeType.sidewinder;
        defaultMaze = false;
    }

    public void binaryTree()
    {
        GameManager.mazeType = GameManager.MazeType.binaryTree;
        defaultMaze = false;
    }

    public void randomMaze()
    {
        defaultMaze = true;
    }
}
