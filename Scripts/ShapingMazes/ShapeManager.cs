using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    List<RandomMaze> mazes;

    void Start()
    {
        mazes = new List<RandomMaze>();
        mazes.Add(new RecursiveBacktracker(Shapes.smileyFace()));
        mazes.Add(new AldousBroder(Shapes.smileyFace()));
        mazes.Add(new HuntAndKill(Shapes.smileyFace()));
        mazes.Add(new BinaryTreeMaze(Shapes.smileyFace(), Cell2D.direction.North, Cell2D.direction.East));
        mazes.Add(new RecursiveBacktracker(Shapes.zigzag()));
        mazes.Add(new AldousBroder(Shapes.zigzag()));
        mazes.Add(new HuntAndKill(Shapes.zigzag()));
        mazes.Add(new BinaryTreeMaze(Shapes.zigzag(), Cell2D.direction.North, Cell2D.direction.East));
        Vector3 mazePos = new Vector3(0, 0, 0);
        int sideLen = 10;
        foreach (RandomMaze maze in mazes)
        {
            RenderMaze renderer = maze.getRenderer(mazePos);
            renderer.drawMaze();
            mazePos += new Vector3(renderer.getUnit() * (sideLen + 5), 0, 0);
        }
    }

}
