using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilsons : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private ArrayList visited;
    private ArrayList unvisited;
    private Stack<Cell2D> path;
    private Grid2D mazePlan;

    public Wilsons(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.mazePlan = new Grid2D(rows, cols);
        this.visited = new ArrayList();
        this.unvisited = new ArrayList();
        initUnvisited();
        this.path = new Stack<Cell2D>();
        genMaze();
    }

    private void initUnvisited()
    {
        foreach (Cell2D cell in mazePlan.getCells())
        {
            unvisited.Add(cell);
        }
    }

    private void markAsVisited(Cell2D cell)
    {
        visited.Add(cell);
        unvisited.Remove(cell);
    }

    private Cell2D randomUnvisited()
    {

        return unvisited[Random.Range(0, unvisited.Count)] as Cell2D;
    }

    private void genMaze()
    {
        Cell2D visitedCell = mazePlan.randomCell() as Cell2D;
        markAsVisited(visitedCell);
        Cell2D startCell = randomUnvisited();
        this.path.Push(startCell);
        genMazeH();
    }

    private void genMazeH()
    {
        Cell2D currentCell = path.Peek();
        if (unvisited.Count > 0)
        {
            if (visited.Contains(currentCell))
            {
                linkPath(); //link, add to visited, empty path, add new start, recur
            }
            else
            {
                Cell2D nextCell = currentCell.randomNeighbor();
                if (path.Contains(nextCell))
                {
                    eraseLoop(nextCell); //erase up to nextCell
                    genMazeH();
                }
                else
                {
                    path.Push(nextCell);
                    genMazeH();
                }
            }
        }
    }

    private void linkPath()
    {
        while (path.Count > 1)
        {
            Cell2D cell1 = path.Pop();
            Cell2D cell2 = path.Peek();
            cell1.Link(cell2);
            markAsVisited(cell2);
        }
        if (path.Count > 0)
            path.Pop();
        if (unvisited.Count > 0)
        {
            Cell2D newStart = randomUnvisited();
            path.Push(newStart);
            genMazeH();
        }
    }

    private void eraseLoop(Cell loopStart)
    {
        while (!(path.Peek().Equals(loopStart)))
        {
            path.Pop();
        }
    }

    private string iterableToString(IEnumerable iter)
    {
        string str = null;
        foreach (var var in iter)
        {
            if (str is null) str = var.ToString();
            else str += ";  " + var.ToString();
        }
        return str;
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
