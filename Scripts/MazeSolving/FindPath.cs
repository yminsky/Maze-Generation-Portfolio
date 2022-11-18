using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
    List<Cell2D.direction> directions;
    Cell2D from;
    Cell2D to;
    Distances distances;
    Cell2D.direction firstDir;

    public FindPath(Cell2D from, Cell2D to)
    {
        this.from = from;
        this.to = to;
        this.directions = new List<Cell2D.direction>();
        //  print("to: " + this.to + ", to North: " + this.to.getNeighbor(Cell.direction.North));
        this.to.genDistances();
        this.distances = this.to.getDistances();
    }

    public void genDirections()
    {
        genDirectionsH(from);
    }

    private void genDirectionsH(Cell2D cell)
    {
        int dist = distances.GetDist(cell);
        if (!(dist == 0))
        {
            foreach (Cell2D link in cell.GetLinks())
            {
                if (distances.GetDist(link) == dist - 1)
                {
                    directions.Add(cell.getRelation(link));
                    genDirectionsH(link);
                }
            }
        }
    }

    public void genFirstDir()
    {
        int dist = distances.GetDist(this.from);

        foreach (Cell2D link in this.from.GetLinks())
        {
            if (distances.GetDist(link) == dist - 1)
            {
                this.firstDir = this.from.getRelation(link);
            }
        }
    }
    public List<Cell2D.direction> GetDirections() => this.directions;

    public Cell2D.direction GetFirstDir() => this.firstDir;

    public Distances GetDistances() => this.distances;

    public Cell2D GetEndpoint() => this.to;

    public Cell2D GetStartpoint() => this.from;
}
