using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RenderMaze2D : MonoBehaviour, RenderMaze
{
    private Grid2D mazePlan;
    private float unit = 10;

    public RenderMaze2D(Grid2D mazePlan)
    {
        this.mazePlan = mazePlan;
    }

    public Grid getMazePlan() => this.mazePlan;

    public float getUnit() => this.unit;

    public void setUnit(float unit)
    {
        this.unit = unit;
    }

    private void prepNonActiveCells()
    {
        List<Cell2D> nonActive = new List<Cell2D>();
        foreach (Cell2D cell in mazePlan.getCells())
        {
            if (!cell.IsActive())
            {
                nonActive.Add(cell);
            }
        }
        foreach (Cell2D cell in nonActive)
        {
            foreach (Cell2D neighbor in genAllNeighbors(cell))
            {
                if (!neighbor.IsActive()) cell.Link(neighbor);
            }
        }
    }

    private List<Cell2D> genAllNeighbors(Cell2D cell)
    {
        List<Cell2D> neighbors = new List<Cell2D>();
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        int row = cell.GetRow();
        int col = cell.GetColumn();
        try { neighbors.Add(cells[row, col + 1]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row, col - 1]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row + 1, col]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row - 1, col]); }
        catch (IndexOutOfRangeException) { }
        //string toPrint = cell + " neighbors: ";
        //foreach (Cell2D neighbor in neighbors) toPrint += neighbor + "  ";
        //print(toPrint);
        return neighbors;
    }

    public void drawMaze()
    {
        prepNonActiveCells();
        int rows = this.mazePlan.getRows();
        int cols = this.mazePlan.getCols();
        for (int row = 0; row <= rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (isNSWall(row, col))
                {
                    GameObject nswall = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                    nswall.transform.localScale = new Vector3(unit, unit, 0.2f);
                    nswall.transform.position = new Vector3(unit * col, 0, unit * row);
                }
            }
        }
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col <= cols; col++)
            {
                if (isEWWall(row, col))
                {
                    GameObject ewwall = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                    ewwall.transform.localScale = new Vector3(0.2f, unit, unit);
                    ewwall.transform.position = new Vector3(unit * (col - 0.5f), 0, unit * (row + 0.5f));
                }
            }
        }
    }

    private bool isNSWall(int row, int col)
    {
        int rows = this.mazePlan.getRows();
        Cell2D[,] cells = this.mazePlan.getCells() as Cell2D[,];
        bool edgeInUse = isNSEdgeInUse(row, col, rows, cells);
        if (!(row == rows))
        {
            Cell2D cell = cells[row, col];
            return !(cell.IsLinked(cell.getNeighbor(Cell2D.direction.South))) && edgeInUse;
        }
        else return edgeInUse;
    }

    private bool isNSEdgeInUse(int row, int col, int rows, Cell2D[,] cells)
    {
        Cell2D cell;
        if (row == rows)
        {
            cell = cells[rows - 1, col];
        }
        else if (row == 0)
        {
            cell = cells[row, col];
        }
        else return true;
        return cell.IsActive();
    }

    private bool isEWWall(int row, int col)
    {
        int cols = this.mazePlan.getCols();
        Cell2D[,] cells = this.mazePlan.getCells() as Cell2D[,];
        bool edgeInUse = isEWEdgeInUse(row, col, cols, cells);
        if (!(col == cols))
        {
            Cell2D cell = cells[row, col];
            return !(cell.IsLinked(cell.getNeighbor(Cell2D.direction.West))) && edgeInUse;
        }
        else return edgeInUse;
    }

    private bool isEWEdgeInUse(int row, int col, int cols, Cell2D[,] cells)
    {
        Cell2D cell;
        if (col == cols)
        {
            cell = cells[row, col - 1];
        }
        else if (col == 0)
        {
            cell = cells[row, col];
        }
        else return true;
        return cell.IsActive();
    }
}