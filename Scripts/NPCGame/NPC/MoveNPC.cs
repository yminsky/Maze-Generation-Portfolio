using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    new public static RenderMaze2D renderer;
    public static FindPath path;
    public static float unit;
    public static Grid2D mazePlan;
    static Cell2D.direction nextDir;
    static Dictionary<Cell2D.direction, int> turning;
    public static Cell2D latestCell;
    public static Cell2D thisTickCell;
    public static bool nextCell = false;
    const float speed = 0.1f;
    static int tick = 0;
    static int cellChangeTick = 100;
    static bool fleeing = false;
    bool timeToTurn = false;
    Cell2D playerMazePos;

    void Start()
    {
        turning = new Dictionary<Cell2D.direction, int>();
        setTurning();
        gameObject.transform.position = NPCMain.randomPos();
        latestCell = GetMazePos(gameObject.transform.position);
        GameManager.NPC = gameObject;
    }

    void Update()
    {
        tick = GameManager.tick;
        playerMazePos = GetMazePos(GameManager.player.transform.position);
        thisTickCell = GetMazePos(gameObject.transform.position);
        nextCell = !(latestCell.Equals(thisTickCell));
        timeToTurn = tick - cellChangeTick == Mathf.FloorToInt(1 / speed);
        if (tick == 0)
        {
            turn(playerMazePos);
        }
        if (nextCell)
        {
            cellChangeTick = tick;
            latestCell = thisTickCell;
        }
        //print("about to move");
        bool playerClose = getDist(NPCMain.mazeToCoord(thisTickCell.GetRow(), thisTickCell.GetColumn()),
                                   NPCMain.mazeToCoord(playerMazePos.GetRow(), playerMazePos.GetColumn())) < 15;
        if (PlayerShoot.shooting && !fleeing && playerClose)
        {
            //print("fleeing");
            StartCoroutine(flee());
            fleeing = true;
        }
        else Pursue(); //print("persuing"); }
        //Pursue();
    }

    private void Pursue()
    {
        if (tick % 100 == 0 && tick - cellChangeTick > 100)
        {
            //print("unstickng");
            turn(playerMazePos);
        }
        //if (tick - cellChangeTick == Mathf.FloorToInt(1 / speed))
        if (timeToTurn)
        {
            turn(playerMazePos);
        }
        float distToPlayer = getDist(GameManager.player.transform.position, gameObject.transform.position);
        //print("should be moving unless BOTH are true: " + (distToPlayer < 10) + ", " + NPCShoot.canSeePlayer);
        if (distToPlayer < 6 && NPCShoot.canSeePlayer && !fleeing)
        {
            //print("stopping");
            gameObject.transform.Translate(Vector3.forward * speed / 400, Space.Self);
        }
        else
            gameObject.transform.Translate(Vector3.forward * speed, Space.Self);

        //else print("stopping");
    }

    public float getDist(Vector3 pos1, Vector3 pos2)
    {
        float x1 = pos1.x;
        float z1 = pos1.z;
        float x2 = pos2.x;
        float z2 = pos2.z;
        float dist = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((z1 - z2), 2));
        return dist;
    }
    private IEnumerator flee()
    {
        //print("fleeing");
        Vector3 myPos = gameObject.transform.position;
        Vector3 playerPos = GameManager.player.transform.position;
        Cell2D myMazePos = GetMazePos(myPos);
        Cell2D target = GetMazePos(NPCMain.randomPos());
        bool cellIsBehind = false;
        float dist = getDist(playerPos, myPos);
        FindPath testPath;
        bool IAmCornered = true;
        foreach (Cell2D link in myMazePos.GetLinks())
        {
            Vector3 linkPos = NPCMain.mazeToCoord(link.GetRow(), link.GetColumn());
            if (getDist(playerPos, linkPos) > dist) IAmCornered = false; //used to be < but that makes no sense
        }
        //print("I am cornered: " + IAmCornered);
        if (!IAmCornered)
        {
            int count = 0;
            while (!cellIsBehind)
            {
                target = GetMazePos(NPCMain.randomPos());
                testPath = new FindPath(myMazePos, target);
                testPath.genFirstDir();
                Cell2D.direction nextDir = testPath.GetFirstDir();
                Cell2D nextCell = myMazePos.getNeighbor(nextDir);
                Vector3 newPos = NPCMain.mazeToCoord(nextCell.GetRow(), nextCell.GetColumn());
                if (getDist(playerPos, newPos) > dist)
                {
                    cellIsBehind = true;
                }
                if (count > 100)
                {
                    //print("over 100 times, target: " + target +
                    //    ", testPath is null: " + (testPath is null) +
                    //    "playerMazePos: " + playerMazePos +
                    //    ", new pos: " + newPos +
                    //    ", dist: " + dist + ", new dist: " + getDist(playerPos, newPos));
                    break;
                }
                count++;
            }
        }
        turn(target);
        while (PlayerShoot.shooting)
        {
            if (timeToTurn)
            {
                turn(target);
            }
            yield return new WaitForEndOfFrame();
        }
        fleeing = false;
    }

    private void turn(Cell2D target)
    {
        //print("target: " + target);
        Cell2D myMazePos = GetMazePos(gameObject.transform.position);
        path = new FindPath(myMazePos, target);
        path.genFirstDir();
        nextDir = path.GetFirstDir();
        float angle = turning[nextDir];
        Vector3 axis = Vector3.up;
        Quaternion newRotation = Quaternion.AngleAxis(angle, axis);
        gameObject.transform.rotation = newRotation;
    }

    private void setTurning()
    {
        turning.Add(Cell2D.direction.North, 0);
        turning.Add(Cell2D.direction.East, 90);
        turning.Add(Cell2D.direction.South, 180);
        turning.Add(Cell2D.direction.West, 270);
    }

    public static Cell2D GetMazePos(Vector3 pos)
    {
        float z = pos.z;
        float x = pos.x + unit / 2;
        int row = Mathf.FloorToInt(z / unit);
        int col = Mathf.FloorToInt(x / unit);
        Grid2D mazePlan = renderer.getMazePlan() as Grid2D;
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        Cell2D cell = cells[row, col];
        return cell;
    }
}