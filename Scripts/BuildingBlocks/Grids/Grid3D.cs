using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D : MonoBehaviour, Grid
{
    private int rows;
    private int cols;
    private int levels;
    private Cell3D[,,] cells;
    private ArrayList activeCells;

    public Grid3D(int rows, int cols, int levels)
    {
        this.rows = rows;
        this.cols = cols;
        this.levels = levels;
        this.cells = prepareGrid();
        configureCells();
        this.activeCells = new ArrayList();
    }

    private Cell3D[,,] prepareGrid()
    {
        Cell3D[,,] cells = new Cell3D[rows, cols, levels];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                for (int level = 0; level < levels; level++)
                {
                    cells[row, col, level] = new Cell3D(row, col, level);
                }
            }
        }
        return cells;
    }

    public void configureCells()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                for (int level = 0; level < levels; level++)
                {
                    Cell3D cell = cells[row, col, level];
                    if (!(row == 0)) { cell.SetNeighbor(Cell3D.direction.South, cells[row - 1, col, level]); }
                    if (!(row == rows - 1)) { cell.SetNeighbor(Cell3D.direction.North, cells[row + 1, col, level]); }
                    if (!(col == 0)) { cell.SetNeighbor(Cell3D.direction.West, cells[row, col - 1, level]); }
                    if (!(col == cols - 1)) { cell.SetNeighbor(Cell3D.direction.East, cells[row, col + 1, level]); }
                    if (!(level == 0)) { cell.SetNeighbor(Cell3D.direction.Down, cells[row, col, level - 1]); }
                    if (!(level == levels - 1)) { cell.SetNeighbor(Cell3D.direction.Up, cells[row, col, level + 1]); }
                }
            }
        }
    }

    public void genActiveCells()
    {
        activeCells = new ArrayList();
        foreach (Cell3D cell in cells)
        {
            if (cell.IsActive())
            {
                activeCells.Add(cell);
            }
        }
    }

    public ArrayList getActiveCells() => activeCells;

    public Cell randomCell() =>
        cells[Random.Range(0, rows),
              Random.Range(0, cols),
              Random.Range(0, levels)];

    public IEnumerable getCells() => cells;

    public int getRows() => rows;

    public int getCols() => cols;

    public int getLevels() => levels;

}
