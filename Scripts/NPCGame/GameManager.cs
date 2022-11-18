using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static RandomMaze maze;
    private static Grid2D mazePlan;
    public static new RenderMaze2D renderer;
    public static int rows = 10;
    public static int cols = 10;
    public static int unit = 3;
    // public static int ticksLeft = 2000;
    public static int tick = 0;
    public static GameObject player;
    public static GameObject NPC;
    public const int keyCount = 4;
    public static float volume;
    public static MazeType mazeType;
    public static bool gameWon;

    void Start()
    {
        if (Settings.defaultMaze)
        {
            mazeType = randomMazeType();
        }
        if (Settings.defaultVol)
        {
            volume = 1;
        }
        makeKeys(keyCount);
        genMaze(mazeType);
        mazePlan = maze.getMazePlan() as Grid2D;
        renderer = new RenderMaze2D(mazePlan);
        renderer.setUnit(unit);
        renderer.drawMaze();
        MoveNPC.renderer = renderer;
        MoveNPC.unit = unit;
        MoveNPC.mazePlan = renderer.getMazePlan() as Grid2D;
        Grid2D testG = new Grid2D(5, 5);
        Camera.main.gameObject.GetComponent<AudioSource>().volume = volume;
        Camera.main.gameObject.GetComponent<AudioSource>().Play();
        player.GetComponent<AudioSource>().volume = volume;
        NPC.GetComponent<AudioSource>().volume = volume;
        //FindPath path = new FindPath(testG.getCells()[3, 4], new Cell(4, 4));   
    }

    private static MazeType randomMazeType()
    {
        int random = Random.Range(0, 3);
        if (random == 0) return MazeType.backtracker;
        else if (random == 1) return MazeType.sidewinder;
        else return MazeType.binaryTree;
    }
    private void genMaze(MazeType mazeType)
    {
        if (mazeType == MazeType.backtracker)
        {
            maze = new RecursiveBacktracker(rows, cols);
        }
        else if (mazeType == MazeType.sidewinder)
        {
            maze = new Sidewinder(rows, cols);
        }
        else maze = new BinaryTreeMaze(rows, cols, Cell2D.direction.North, Cell2D.direction.East);
    }

    public enum MazeType
    {
        backtracker,
        sidewinder,
        binaryTree
    }
    void Update()
    {
        tick++;
        //Follow.GetMazePos(player.transform.position);
        //print(Follow.GetMazePos(player.transform.position));
    }

    private void testMazeToCoord()
    {
        //testing NPCMain.mazeToCoord
        print("0, 0: " + NPCMain.mazeToCoord(0, 0));
        print("1, 0: " + NPCMain.mazeToCoord(1, 0));
        print("0, 1: " + NPCMain.mazeToCoord(0, 1));
        print("5, 5: " + NPCMain.mazeToCoord(5, 5));
    }

    private void testBuiltIns()
    {
        print("mouse: " + Input.mousePosition);
        print("mouse in world: " + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)));
    }

    private static void makeKeys(int n)
    {
        if (n > 0)
        {
            GameObject key = Resources.Load("key_gold") as GameObject;
            //key.AddComponent<BoxCollider>();
            key.AddComponent<Keys>();
            key.tag = "Key";
            key.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
            Instantiate(key);
            makeKeys(n - 1);
        }
    }

    public static void makeDoor()
    {
        GameObject door = Resources.Load("Door") as GameObject;
        //door.AddComponent<BoxCollider>();
        door.AddComponent<Door>();
        door.tag = "Door";
        Instantiate(door);
    }

}