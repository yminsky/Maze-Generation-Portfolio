using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecursiveBacktracker : MonoBehaviour, RandomMaze
{
    private Grid grid;
    private int cellCount = 1;
    private Tools.dimensions d;

    public RecursiveBacktracker(int rows, int cols)
    {
        this.grid = new Grid2D(rows, cols);
        this.d = Tools.dimensions.TwoD;
        genMaze();
    }

    public RecursiveBacktracker(int rows, int cols, int levels)
    {
        this.grid = new Grid3D(rows, cols, levels);
        this.d = Tools.dimensions.ThreeD;
        genMaze();
    }

    public RecursiveBacktracker(Grid2D grid)
    {
        this.grid = grid;
        this.d = grid.dimensions();
        genMaze();
    }

    private void genMaze()
    {
        this.grid.genActiveCells();
        IEnumerable cells = this.grid.getCells();
        Stack<Cell> stack = new Stack<Cell>();
        Cell firstCell = new Cell2D(0, 0);
        foreach (Cell cell in cells)
        {
            if (cell.IsActive())
            {
                firstCell = cell;
                break;
            }
        }
        //go through left to right from 00 and then up
        stack.Push(firstCell);
        genMazeH(cells, stack);
    }

    private void genMazeH(IEnumerable cells, Stack<Cell> stack)
    {
        //print("starting genMazeH");
        if (!(cellCount == this.grid.getActiveCells().Count))
        {
            Cell currentCell = stack.Peek();
            List<Cell> neighbors = new List<Cell>();
            IEnumerable allNeighbors = new ArrayList();
            try
            {
                Cell2D cell = currentCell as Cell2D;
                allNeighbors = cell.getNeighbors().Values;
            }
            catch (NullReferenceException)
            {
                //print("not 2D"); 
            }
            try
            {
                Cell3D cell = currentCell as Cell3D;
                allNeighbors = cell.GetNeighbors().Values;
            }
            catch (NullReferenceException) { print("not 3D"); }
            foreach (Cell neighbor in allNeighbors)
            {
                //print("deciding whether to link");
                if (neighbor.GetLinks().Count == 0)
                    neighbors.Add(neighbor);
            }
            if (neighbors.Count == 0)
            {
                //print("no neighbors");
                stack.Pop();
                genMazeH(cells, stack);
            }
            else
            {
                Cell nextCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                if (d == Tools.dimensions.TwoD)
                {
                    Cell2D toLink = nextCell as Cell2D;
                    Cell2D me = currentCell as Cell2D;
                    me.Link(toLink);
                }
                else
                {
                    Cell3D toLink = nextCell as Cell3D;
                    Cell3D me = currentCell as Cell3D;
                    me.Link(toLink);
                }
                stack.Push(nextCell);
                cellCount++;
                genMazeH(cells, stack);
            }

        }
    }

    public Grid getGrid() => this.grid;

    public Tools.dimensions getDimensions() => d;

    public RenderMaze getRenderer()
    {
        if (d == Tools.dimensions.TwoD)
        {
            return new RenderMaze2D(grid as Grid2D);
        }
        else
        {
            return new RenderMaze3D(grid as Grid3D);
        }
    }

    public RenderMaze getRenderer(Vector3 pos)
    {
        if (d == Tools.dimensions.TwoD)
        {
            return new RenderMaze2D(grid as Grid2D, pos);
        }
        else
        {
            return new RenderMaze3D(grid as Grid3D, pos);
        }
    }
}
