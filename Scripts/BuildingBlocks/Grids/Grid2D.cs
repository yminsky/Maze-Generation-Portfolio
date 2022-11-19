using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : Grid
{
    private int rows;
    private int columns;
    private Cell2D[,] cells;
    private ArrayList activeCells;

    public Grid2D(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.cells = prepareGrid();
        configureCells();
        this.activeCells = new ArrayList();
    }

    private Cell2D[,] prepareGrid()
    {
        Cell2D[,] cells = new Cell2D[this.rows, this.columns];
        for (int row = 0; row < this.rows; row++)
        {
            for (int column = 0; column < this.columns; column++)
            {
                cells[row, column] = new Cell2D(row, column);
            }
        }
        return cells;
    }

    public void genActiveCells()
    {
        activeCells = new ArrayList();
        foreach (Cell2D cell in this.cells)
        {
            if (cell.IsActive())
            {
                activeCells.Add(cell);
            }
        }
    }

    public ArrayList getActiveCells() => this.activeCells;

    public void configureCells()
    {
        for (int row = 0; row < this.rows; row++)
        {
            for (int column = 0; column < this.columns; column++)
            {
                Cell2D cell = this.cells[row, column];
                if (!(row == 0)) { cell.setNeighbor(Cell2D.direction.South, this.cells[row - 1, column]); }
                if (!(row == this.rows - 1)) { cell.setNeighbor(Cell2D.direction.North, this.cells[row + 1, column]); }
                if (!(column == 0)) { cell.setNeighbor(Cell2D.direction.West, this.cells[row, column - 1]); }
                if (!(column == this.columns - 1)) { cell.setNeighbor(Cell2D.direction.East, this.cells[row, column + 1]); }
            }
        }
    }

    public Cell randomCell()
    {
        int randR = Random.Range(0, rows);
        int randC = Random.Range(1, columns);
        return this.cells[randR, randC];
    }

    public int getSize() => this.rows * this.columns;

    private bool isEdge(int row, int column)
    {
        return row == 0 || row == this.rows - 1 || column == 0 || column == this.columns - 1;
    }

    //since I can't figure out how to make a method that can take a method this'll have to do

    public IEnumerable getCells() => this.cells;
    public void setCells(Cell2D[,] cells)
    {
        this.cells = cells;
        configureCells();
    }

    public int getRows() => this.rows;

    public int getCols() => this.columns;

    public Tools.dimensions dimensions() => Tools.dimensions.TwoD;

}
