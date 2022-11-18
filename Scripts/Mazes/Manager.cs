using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    RandomMaze maze;
    new RenderMaze renderer;

    public static int tick;
    void Start()
    {
        //maze = new AldousBroder(10, 10);
        //maze = new Wilsons(10, 10);
        //maze = new HuntAndKill(100, 100);
        //maze = new BinaryTreeMaze(10, 10, Cell2D.direction.South, Cell2D.direction.West);
        //maze = new RecursiveBacktracker(10, 10);
        //renderer = new RenderMaze2D(maze.getMazePlan() as Grid2D);
        maze = new RecursiveBacktracker(10, 5, 10);
        renderer = new RenderMaze3D(maze.getMazePlan() as Grid3D);
        renderer.drawMaze();
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
        Cell2D[,] cells = maze.getMazePlan().getCells() as Cell2D[,];
        List<Cell> links = cells[0, 0].GetLinks();
        print("should be true: " + links.Contains(links[0]));
        print("should be false: " + links.Contains(new Cell2D(100, 100)));
    }
}
