using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldousBroder : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private ArrayList visited;
    private Grid2D grid;

    public AldousBroder(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.visited = new ArrayList();
        this.grid = new Grid2D(rows, cols);
        genMaze();
    }

    public AldousBroder(Grid2D grid)
    {
        this.rows = grid.getRows();
        this.cols = grid.getCols();
        this.visited = new ArrayList();
        this.grid = grid;
        genMaze();
    }

    private void genMaze()
    {
        grid.genActiveCells();
        ArrayList activeCells = grid.getActiveCells();
        Cell2D firstCell = activeCells[Random.Range(0, activeCells.Count)] as Cell2D;
        visited.Add(firstCell);
        genMazeH(firstCell);
    }

    private void genMazeH(Cell2D currentCell)
    {
        if (this.visited.Count != this.grid.getActiveCells().Count)
        {
            Cell2D nextCell = currentCell.randomNeighbor();
            if (!(visited.Contains(nextCell)))
            {
                currentCell.Link(nextCell);
                visited.Add(nextCell);
            }
            genMazeH(nextCell);
        }
    }

    public Grid getGrid() => this.grid;

    public RenderMaze getRenderer()
    {
        return new RenderMaze2D(grid);
    }

    public RenderMaze getRenderer(Vector3 pos)
    {
        return new RenderMaze2D(grid, pos);
    }

}
