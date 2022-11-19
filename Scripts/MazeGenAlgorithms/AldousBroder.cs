using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldousBroder : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private ArrayList visited;
    private Grid2D mazePlan;

    public AldousBroder(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.visited = new ArrayList();
        this.mazePlan = new Grid2D(rows, cols);
        genMaze();
    }

    public AldousBroder(Grid2D mazePlan)
    {
        this.rows = mazePlan.getRows();
        this.cols = mazePlan.getCols();
        this.visited = new ArrayList();
        this.mazePlan = mazePlan;
        genMaze();
    }

    private void genMaze()
    {
        mazePlan.genActiveCells();
        ArrayList activeCells = mazePlan.getActiveCells();
        Cell2D firstCell = activeCells[Random.Range(0, activeCells.Count)] as Cell2D;
        visited.Add(firstCell);
        genMazeH(firstCell);
    }

    private void genMazeH(Cell2D currentCell)
    {
        if (this.visited.Count != this.mazePlan.getActiveCells().Count)
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

    public Grid getMazePlan() => this.mazePlan;

    public RenderMaze getRenderer()
    {
        return new RenderMaze2D(mazePlan);
    }

    public RenderMaze getRenderer(Vector3 pos)
    {
        return new RenderMaze2D(mazePlan, pos);
    }

}
