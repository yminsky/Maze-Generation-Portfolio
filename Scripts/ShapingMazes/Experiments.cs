using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiments : MonoBehaviour
{
    Grid2D grid;
    const int rows = 10;
    const int cols = 10;
    new RenderMaze2D renderer;
    RandomMaze maze;

    // if you can, make there be two options for a constructer when making a new random maze
    // one option for giving it dimensions (i.e. row, col, etc.)
    // and one option for giving it a ready made grid
    // or maybe instead of a grid you give it a Cell2D[,] but don't really see any advantages to that

    //try to find a way to fix the binary tree alg (or else take away the second constructor?)

    void Start()
    {
        grid = new Grid2D(rows, cols);
        changeShape();
        Cell2D[,] cells = grid.getCells() as Cell2D[,];
        remove(cells[2, 0]);
        //remove(cells[2, 1]);
        remove(cells[3, 3]);
        remove(cells[3, 2]);
        remove(cells[2, 3]);
        remove(cells[2, 2]);
        //maze = new BinaryTreeMaze(grid, Cell2D.direction.North, Cell2D.direction.East);
        //maze = new RecursiveBacktracker(grid);
        maze = new AldousBroder(grid);
        renderer = new RenderMaze2D(grid);
        //print(cells[2, 1].isInUse());
        renderer.drawMaze();
    }

    private void changeShape()
    {
        Cell2D[,] cells = grid.getCells() as Cell2D[,];
        foreach (Cell2D cell in cells)
        {
            if (isCorner(cell))
            {
                remove(cell);
            }
        }
    }

    public Grid2D getMaze() => grid;
    private bool isCorner(Cell2D cell)
    {
        int row = cell.GetRow();
        int col = cell.GetColumn();
        return (row == 0 || row == rows - 1) && (col == 0 || col == cols - 1);
    }

    private void remove(Cell2D cell)
    {
        cell.setUse(false);
    }
}
