using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HuntAndKill : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private Grid2D grid;
    private ArrayList activeCells;
    private bool finished;

    public HuntAndKill(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.grid = new Grid2D(rows, cols);
        this.grid.genActiveCells();
        this.activeCells = this.grid.getActiveCells();
        this.finished = false;
        genMaze();
    }

    public HuntAndKill(Grid2D grid)
    {
        this.rows = grid.getRows();
        this.cols = grid.getCols();
        this.grid = grid;
        this.grid.genActiveCells();
        this.activeCells = this.grid.getActiveCells();
        this.finished = false;
        genMaze();
    }

    private void genMaze()
    {
        Cell2D startCell = activeCells[Random.Range(0, activeCells.Count)] as Cell2D;
        genMazeH(startCell);
    }

    private void genMazeH(Cell2D cell)
    {
        ArrayList unvisitedNeighbors = new ArrayList();
        Cell2D nextCell;
        foreach (Cell2D neighbor in cell.getNeighbors().Values)
        {
            if (!cellIsVisited(neighbor))
            {
                unvisitedNeighbors.Add(neighbor);
            }
        }
        if (unvisitedNeighbors.Count == 0)
        {
            nextCell = hunt(); //links in method
        }
        else
        {
            nextCell = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)] as Cell2D;
            cell.Link(nextCell);
            genMazeH(nextCell);
        }
        if (!finished)
        {
            genMazeH(nextCell);
        }
    }

    private Cell2D hunt()
    {
        foreach (Cell2D cell in activeCells)
        {
            if (!cellIsVisited(cell))
            {
                ArrayList visitedNeighbors = new ArrayList();
                foreach (Cell2D neighbor in cell.getNeighbors().Values)
                {
                    if (cellIsVisited(neighbor)) { visitedNeighbors.Add(neighbor); }
                }
                Cell2D nextCell = cell;
                if (visitedNeighbors.Count > 0)
                {
                    cell.Link(visitedNeighbors[Random.Range(0, visitedNeighbors.Count)] as Cell2D);
                    return nextCell;
                }
            }
        }
        this.finished = true;
        return new Cell2D(0, 0);
    }

    private bool cellIsVisited(Cell2D cell)
    {
        return cell.GetLinks().Count > 0;
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
