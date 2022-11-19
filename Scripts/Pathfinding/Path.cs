using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    List<Cell3D.direction> directions;
    Cell3D from;
    Cell3D to;
    Distances distances;
    Cell3D.direction firstDir;
    //if you decide to make this work for 3D and 2D directions are the tricky part. You're gonna need a field in the constructor for what dimension its in

    public Path(Cell3D from, Cell3D to)
    {
        this.from = from;
        this.to = to;
        this.directions = new List<Cell3D.direction>();
        //  print("to: " + this.to + ", to North: " + this.to.getNeighbor(Cell.direction.North));
        this.to.GenDistances();
        this.distances = this.to.GetDistances();
    }

    public void genDirections()
    {
        genDirectionsH(from);
    }

    private void genDirectionsH(Cell3D cell)
    {
        int dist = distances.getDist(cell);
        if (!(dist == 0))
        {
            foreach (Cell3D link in cell.GetLinks())
            {
                if (distances.getDist(link) == dist - 1)
                {
                    directions.Add(cell.GetRelation(link));
                    genDirectionsH(link);
                }
            }
        }
    }

    public void genFirstDir()
    {
        int dist = distances.getDist(this.from);

        foreach (Cell3D link in this.from.GetLinks())
        {
            if (distances.getDist(link) == dist - 1)
            {
                this.firstDir = this.from.GetRelation(link);
            }
        }
    }
    public List<Cell3D.direction> getDirections() => this.directions;

    public Cell3D.direction getFirstDir() => this.firstDir;

    public Distances getDistances() => this.distances;

    public Cell3D getEndpoint() => this.to;

    public Cell3D getStartpoint() => this.from;
}
