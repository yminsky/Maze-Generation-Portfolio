using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaper2D : MonoBehaviour
{
    private Grid2D grid;

    public Shaper2D(Grid2D grid)
    {
        this.grid = grid;
    }

    public Shaper2D(int rows, int columns)
    {
        this.grid = new Grid2D(rows, columns);
    }

    public void remove(int row, int col)
    {
        Cell2D[,] cells = grid.getCells() as Cell2D[,];
        cells[row, col].SetStatus(false);
    }

    public void removeCorners()
    {
        int rows = grid.getRows(); int cols = grid.getCols();
        remove(0, 0); remove(0, cols - 1); remove(rows - 1, 0); remove(rows - 1, cols - 1);
    }

    public void removeSquare(int sideLen, int firstRow, int firstCol)
    {
        for (int r = firstRow; r < sideLen + firstRow; r++)
        {
            for (int c = firstCol; c < sideLen + firstCol; c++)
            {
                remove(r, c);
            }
        }
    }

    public void removeRowLine(int len, int row, int firstCol)
    {
        for (int c = firstCol; c < len + firstCol; c++)
        {
            remove(row, c);
        }
    }

    public void removeColLine(int len, int firstRow, int col)
    {
        for (int r = firstRow; r < len + firstRow; r++)
        {
            remove(r, col);
        }
    }

    public Grid2D getGrid() => grid;

}
