using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeMaze : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private Cell2D.direction ver;
    private Cell2D.direction hor;
    private Grid2D mazePlan;

    public BinaryTreeMaze(int rows, int cols, Cell2D.direction ver, Cell2D.direction hor)
    {
        this.rows = rows;
        this.cols = cols;
        this.ver = ver;
        this.hor = hor;
        this.mazePlan = new Grid2D(rows, cols);
        genMaze();
    }

    public BinaryTreeMaze(Grid2D mazePlan, Cell2D.direction ver, Cell2D.direction hor)
    {
        this.rows = mazePlan.getRows();
        this.cols = mazePlan.getCols();
        this.ver = ver;
        this.hor = hor;
        this.mazePlan = mazePlan;
        genMaze();
    }

    private void genMaze()
    {
        mazePlan.genActiveCells();
        ArrayList blueprint = mazePlan.getActiveCells();
        foreach (Cell2D cell in blueprint)
        {
            int row = cell.GetRow();
            int col = cell.GetColumn();
            Cell2D horVal = cell.getNeighbor(hor);
            Cell2D verVal = cell.getNeighbor(ver);
            List<Cell2D> neighbors = new List<Cell2D>();
            int count = 0;
            if (!(horVal is null))
            {
                if (horVal.IsActive()) { neighbors.Add(horVal); count++; }
            }
            if (!(verVal is null))
            {
                if (verVal.IsActive()) { neighbors.Add(verVal); count++; }
            }

            if (!(count == 0))
            {
                Cell2D neighbor = neighbors[Random.Range(0, count)];
                cell.Link(neighbor);
            }
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

