using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    List<RandomMaze> mazes;
    const int sideLen = 10;
    public static int tick;

    void Start()
    {
        mazes = new List<RandomMaze>();
        mazes.Add(new AldousBroder(sideLen, sideLen));
        mazes.Add(new Wilsons(sideLen, sideLen));
        mazes.Add(new HuntAndKill(sideLen, sideLen));
        mazes.Add(new BinaryTreeMaze(sideLen, sideLen, Cell2D.direction.South, Cell2D.direction.West));
        mazes.Add(new Sidewinder(sideLen, sideLen));
        mazes.Add(new RecursiveBacktracker(sideLen, sideLen));
        mazes.Add(new RecursiveBacktracker(sideLen, sideLen, sideLen));
        Vector3 mazePos = new Vector3(0, 0, 0);
        foreach (RandomMaze maze in mazes)
        {
            RenderMaze renderer = maze.getRenderer(mazePos);
            renderer.drawMaze();
            mazePos += new Vector3(renderer.getUnit() * (sideLen + 5), 0, 0);
        }
        // maze = new RecursiveBacktracker(10, 5, 10);
        // renderer = new RenderMaze3D(maze.getMazePlan() as Grid3D);
        // renderer.drawMaze();
        tick = 0;
    }

    void Update()
    {
        tick++;
    }

    private void testRandomNeighbor()
    {
        Grid2D grid = new Grid2D(5, 5);
        Cell2D randomCell = grid.randomCell() as Cell2D;
        print(randomCell.randomNeighbor());
    }

    private void testCellLinksContains()
    {
        RandomMaze maze = mazes[0];
        Cell2D[,] cells = maze.getMazePlan().getCells() as Cell2D[,];
        List<Cell> links = cells[0, 0].GetLinks();
        print("should be true: " + links.Contains(links[0]));
        print("should be false: " + links.Contains(new Cell2D(100, 100)));
    }
}
