using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeMaze : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private Cell2D.direction ver;
    private Cell2D.direction hor;
    private Grid2D grid;

    public BinaryTreeMaze(int rows, int cols, Cell2D.direction ver, Cell2D.direction hor)
    {
        this.rows = rows;
        this.cols = cols;
        this.ver = ver;
        this.hor = hor;
        this.grid = new Grid2D(rows, cols);
        genMaze();
    }

    public BinaryTreeMaze(Grid2D grid, Cell2D.direction ver, Cell2D.direction hor)
    {
        this.rows = grid.getRows();
        this.cols = grid.getCols();
        this.ver = ver;
        this.hor = hor;
        this.grid = grid;
        genMaze();
    }

    private void genMaze()
    {
        grid.genActiveCells();
        ArrayList blueprint = grid.getActiveCells();
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

