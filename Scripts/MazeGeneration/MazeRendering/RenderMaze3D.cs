using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RenderMaze3D : MonoBehaviour, RenderMaze
{
    private float unit = 10;
    private Grid3D grid;
    private Vector3 pos = new Vector3(0, 0, 0);

    public RenderMaze3D(Grid3D grid)
    {
        this.grid = grid;
    }

    public RenderMaze3D(Grid3D grid, Vector3 pos)
    {
        this.grid = grid;
        this.pos = pos;
    }

    public Grid getGrid() => grid;

    public float getUnit() => unit;

    public void setUnit(float unit)
    {
        this.unit = unit;
    }

    public Vector3 getPos() => pos;

    public void setPos(Vector3 pos)
    {
        this.pos = pos;
    }

    private void prepNonActiveCells()
    {
        List<Cell3D> nonActive = new List<Cell3D>();
        foreach (Cell3D cell in grid.getCells())
        {
            if (!cell.IsActive())
            {
                nonActive.Add(cell);
            }
        }
        foreach (Cell3D cell in nonActive)
        {
            foreach (Cell3D neighbor in genAllNeighbors(cell))
            {
                if (!neighbor.IsActive()) cell.Link(neighbor);
            }
        }
    }

    private List<Cell3D> genAllNeighbors(Cell3D cell)
    {
        List<Cell3D> neighbors = new List<Cell3D>();
        Cell3D[,,] cells = grid.getCells() as Cell3D[,,];
        int row = cell.GetRow();
        int col = cell.GetColumn();
        int level = cell.GetLevel();
        try { neighbors.Add(cells[row, col, level + 1]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row, col, level - 1]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row, col + 1, level]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row, col - 1, level]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row + 1, col, level]); }
        catch (IndexOutOfRangeException) { }
        try { neighbors.Add(cells[row - 1, col, level]); }
        catch (IndexOutOfRangeException) { }
        return neighbors;
    }

    public void drawMaze()
    {
        prepNonActiveCells();
        int rows = grid.getRows();
        int cols = grid.getCols();
        int levels = grid.getLevels();
        for (int row = 0; row <= rows; row++) //should be <=
        {
            for (int col = 0; col < cols; col++)
            {
                for (int level = 0; level < levels; level++)
                {
                    if (isNSWall(row, col, level))
                    {
                        GameObject nswall = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                        nswall.transform.localScale = new Vector3(unit, unit, 0.2f);
                        nswall.transform.position = pos + new Vector3(unit * col, unit * level, unit * row);
                    }
                }
            }
        }
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col <= cols; col++) //should be <=
            {
                for (int level = 0; level < levels; level++)
                {
                    if (isEWWall(row, col, level))
                    {
                        GameObject ewwall = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                        ewwall.transform.localScale = new Vector3(0.2f, unit, unit);
                        ewwall.transform.position = pos + new Vector3(unit * (col - 0.5f), unit * level, unit * (row + 0.5f));
                    }
                }
            }
        }
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                for (int level = 0; level <= levels; level++) //should be <=
                {
                    if (isUDWall(row, col, level))
                    {
                        GameObject udwall = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                        udwall.transform.localScale = new Vector3(unit, 0.2f, unit);
                        udwall.transform.position = pos + new Vector3(unit * col, unit * (level - 0.5f), unit * (row + 0.5f));
                    }
                }
            }
        }
    }

    private bool isNSWall(int row, int col, int level)
    {
        int rows = grid.getRows();
        Cell3D[,,] cells = grid.getCells() as Cell3D[,,];
        bool edgeInUse = isNSEdgeInUse(row, col, level, rows, cells);
        if (!(row == rows))
        {
            Cell3D cell = cells[row, col, level];
            return !(cell.IsLinked(cell.GetNeighbor(Cell3D.direction.South))) && edgeInUse;
        }
        else return edgeInUse;
    }

    private bool isNSEdgeInUse(int row, int col, int level, int rows, Cell3D[,,] cells)
    {
        Cell3D cell;
        if (row == rows)
        {
            cell = cells[rows - 1, col, level];
        }
        else if (row == 0)
        {
            cell = cells[row, col, level];
        }
        else return true;
        return cell.IsActive();
    }

    private bool isEWWall(int row, int col, int level)
    {
        int cols = grid.getCols();
        Cell3D[,,] cells = grid.getCells() as Cell3D[,,];
        bool edgeInUse = isEWEdgeInUse(row, col, level, cols, cells);
        if (!(col == cols))
        {
            Cell3D cell = cells[row, col, level];
            return !(cell.IsLinked(cell.GetNeighbor(Cell3D.direction.West)));
        }
        else return edgeInUse;
    }

    private bool isEWEdgeInUse(int row, int col, int level, int cols, Cell3D[,,] cells)
    {
        Cell3D cell;
        if (col == cols)
        {
            cell = cells[row, col - 1, level];
        }
        else if (col == 0)
        {
            cell = cells[row, col, level];
        }
        else return true;
        return cell.IsActive();
    }

    private bool isUDWall(int row, int col, int level)
    {
        int levels = grid.getLevels();
        Cell3D[,,] cells = grid.getCells() as Cell3D[,,];
        bool edgeInUse = isUDEdgeInUse(row, col, level, levels, cells);
        if (!(level == levels))
        {
            Cell3D cell = cells[row, col, level];
            return !(cell.IsLinked(cell.GetNeighbor(Cell3D.direction.Down)));
        }
        else return edgeInUse;
    }

    private bool isUDEdgeInUse(int row, int col, int level, int levels, Cell3D[,,] cells)
    {
        Cell3D cell;
        if (level == levels)
        {
            cell = cells[row, col, level - 1];
        }
        else if (level == 0)
        {
            cell = cells[row, col, level];
        }
        else return true;
        return cell.IsActive();
    }
}
