using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidewinder : MonoBehaviour, RandomMaze
{
    private int rows;
    private int cols;
    private Grid2D mazePlan;

    List<Cell2D> currentGroup = new List<Cell2D>();
    public Sidewinder(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.mazePlan = genMaze(rows, cols);
    }

    public Grid getMazePlan() => this.mazePlan;

    private Grid2D genMaze(int rows, int cols)
    {
        Grid2D mazePlan = new Grid2D(rows, cols);
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        foreach (Cell2D cell in cells)
        {
            bool topRow = cell.GetRow() == rows - 1;
            bool lastCol = cell.GetColumn() == cols - 1;
            this.currentGroup.Add(cell);
            if (topRow && !lastCol) goEast(cell);
            else if (lastCol && !topRow) goNorth();
            else if (!topRow)
            {
                int dir = Random.Range(0, 2);
                if (dir == 0) goEast(cell);
                else goNorth();
            }
        }
        mazePlan.setCells(cells);
        return mazePlan;
    }

    private void goEast(Cell2D cell)
    {
        Cell2D neighbor = cell.getNeighbor(Cell2D.direction.East);
        cell.Link(neighbor);
        this.currentGroup.Add(neighbor);
    }

    private void goNorth()
    {
        Cell2D groupMem = this.currentGroup[Random.Range(0, this.currentGroup.Count)];
        string preLinks = "";
        foreach (Cell2D link in groupMem.GetLinks())
        {
            preLinks += link + "  ";
        }
        groupMem.Link(groupMem.getNeighbor(Cell2D.direction.North));
        string postLinks = "";
        foreach (Cell2D link in groupMem.GetLinks())
        {
            postLinks += link + "  ";
        }
        this.currentGroup = new List<Cell2D>();
    }
}
