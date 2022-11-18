using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    ArrayList visited;
    void Start()
    {
        test3DRB();
    }

    private void testRend2D()
    {
        Grid2D mazePlan = new Grid2D(2, 2);
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        cells[0, 0].Link(cells[0, 1]);
        cells[0, 1].Link(cells[1, 1]);
        cells[1, 0].Link(cells[1, 1]);
        print("hopefully false: " + cells[1, 0].IsLinked(cells[0, 0]));
        foreach (Cell2D cell in cells)
        {
            string linksPrint = cell.ToString() + ": ";
            foreach (Cell2D link in cell.GetLinks())
            {
                linksPrint += link.ToString() + "  ";
            }
            print(linksPrint);
        }
        RenderMaze2D renderer = new RenderMaze2D(mazePlan);
        renderer.drawMaze();
    }
    private void simBT()
    {
        Grid2D mazePlan = new Grid2D(2, 2);
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        foreach (Cell2D cell in cells)
        {
            int row = cell.GetRow();
            int col = cell.GetColumn();
            Cell2D lonVal = cell.getNeighbor(Cell2D.direction.North);
            Cell2D latVal = cell.getNeighbor(Cell2D.direction.East);
            List<Cell2D> neighbors = new List<Cell2D>();
            int count = 0;
            if (!(lonVal is null)) { neighbors.Add(lonVal); count++; }
            if (!(latVal is null)) { neighbors.Add(latVal); count++; }
            if (!(count == 0))
            {
                Cell2D neighbor = neighbors[Random.Range(0, count)];
                cell.Link(neighbor);
                print(neighbor);
            }
        }
        RenderMaze2D renderer = new RenderMaze2D(mazePlan);
        renderer.drawMaze();
    }
    private void testBT()
    {
        BinaryTreeMaze maze = new BinaryTreeMaze(4, 4, Cell2D.direction.North, Cell2D.direction.East);
        Grid2D mazePlan = maze.getMazePlan() as Grid2D;
        Cell2D[,] cells = mazePlan.getCells() as Cell2D[,];
        foreach (Cell2D cell in cells)
        {
            foreach (Cell2D link in cell.GetLinks())
            {
                // print(link);
            }
            // print(cell.GetLinks().Count == 0);
            // print("North: " + cell.getNeighbor(Cell.direction.North));
        }
        RenderMaze2D renderer = new RenderMaze2D(mazePlan);
        renderer.drawMaze();
    }

    private void initialTests()
    {
        Grid2D maze = new Grid2D(10, 10);
        Cell2D[,] cells = maze.getCells() as Cell2D[,];
        Cell2D a = cells[3, 2];
        Cell2D b = cells[2, 2];
        print(a.IsLinked(b) + "," + b.IsLinked(a));
        a.Link(b);
        print(a.IsLinked(b) + "," + b.IsLinked(a));
        print("South: " + a.getNeighbor(Cell2D.direction.South));
        print("North: " + b.getNeighbor(Cell2D.direction.North));
        a.setNeighbor(Cell2D.direction.South, b);
        b.setNeighbor(Cell2D.direction.North, a);
        print("a: " + a);
        print("b: " + b);
        print("South: " + a.getNeighbor(Cell2D.direction.South));
        print("North: " + b.getNeighbor(Cell2D.direction.North));
    }

    private void test3DRB()
    {
        RecursiveBacktracker maze = new RecursiveBacktracker(10, 10, 10);
        Grid3D grid = maze.getMazePlan() as Grid3D;
        Cell3D[,,] cells = grid.getCells() as Cell3D[,,];
        visited = new ArrayList();
        Cell3D startCell = cells[0, 0, 0];
        visited.Add(startCell);
        search(startCell);
    }

    private void search(Cell3D cell)
    {
        List<Cell> links = cell.GetLinks();
        if (visited.Count == 1000)
        {
            print("success");
        }
        else if (links.Count != 0)
        {
            ArrayList toUnlink = new ArrayList();
            foreach (Cell3D link in links)
            {
                if (visited.Contains(link))
                {
                    print("there's a loop");
                }
                else
                {
                    visited.Add(link);
                    toUnlink.Add(link);
                }
            }
            foreach (Cell3D link in toUnlink)
            {
                cell.Unlink(link);
                search(link);
            }
        }
    }

}
