using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell2D : Cell
{
    private int row;
    private int column;
    private Dictionary<direction, Cell2D> neighbors;
    private List<Cell> links;
    private bool active;
    private Distances distances;

    public Cell2D(int row, int column)
    {
        this.row = row;
        this.column = column;
        this.links = new List<Cell>();
        this.neighbors = new Dictionary<direction, Cell2D>();
        active = true;
        initializeNeighbors();
    }

    private void initializeNeighbors()
    {
        neighbors.Add(direction.North, null);
        neighbors.Add(direction.East, null);
        neighbors.Add(direction.South, null);
        neighbors.Add(direction.West, null);
    }

    public bool IsActive() => this.active;

    public void setUse(bool active)
    {
        this.active = active;
    }

    public override string ToString()
    {
        return row.ToString() + ", " + column.ToString();
    }

    //bidi: bidirectional
    public void Link(Cell2D cell)
    {
        LinkH(cell, true);
    }
    private void LinkH(Cell2D cell, bool bidi)
    {
        links.Add(cell);
        if (bidi)
        {
            cell.LinkH(this, false);
        }
    }
    public void Unlink(Cell2D cell)
    {
        UnlinkH(cell, true);
    }

    private void UnlinkH(Cell2D cell, bool bidi)
    {
        this.links.Remove(cell);
        if (bidi)
        {
            cell.UnlinkH(this, false);
        }
    }

    public List<Cell> GetLinks() => this.links;

    public bool IsLinked(Cell2D cell) => this.links.Contains(cell);

    public override bool Equals(object other)
    {
        if (other is Cell2D)
        {
            Cell2D cell = other as Cell2D;
            return this.row == cell.GetRow() && this.column == cell.GetColumn();
        }
        else return false;
    }

    public override int GetHashCode()
    {
        return row.GetHashCode() + column.GetHashCode();
    }

    public int GetColumn() => column;

    public int GetRow() => row;

    public Dictionary<direction, Cell2D> getNeighbors()
    {
        Dictionary<direction, Cell2D> dict = new Dictionary<direction, Cell2D>();
        foreach (direction key in this.neighbors.Keys)
        {
            Cell2D val = this.neighbors[key];
            if (!(val is null))
            {
                if (val.IsActive()) dict.Add(key, val);
            }
        }
        return dict;
    }

    public Cell2D randomNeighbor()
    {
        List<Cell2D> opts = new List<Cell2D>();
        foreach (Cell2D neighbor in getNeighbors().Values)
        {
            opts.Add(neighbor);
        }
        return opts[UnityEngine.Random.Range(0, opts.Count)];
    }

    public Cell2D getNeighbor(direction dir) => neighbors[dir];

    public void setNeighbor(direction dir, Cell2D neighbor)
    {
        neighbors[dir] = neighbor;
    }

    public direction getRelation(Cell2D cell)
    {
        if (!neighbors.ContainsValue(cell))
        {
            throw new ArgumentException("argument must be one of the cell's neighbors");
        }
        if (cell.Equals(neighbors[direction.North]))
        {
            return direction.North;
        }
        if (cell.Equals(neighbors[direction.East]))
        {
            return direction.East;
        }
        if (cell.Equals(neighbors[direction.South]))
        {
            return direction.South;
        }
        else { return direction.West; }
    }
    public enum direction
    {
        North,
        East,
        South,
        West
    }

    //finding solutions (Dijkstra's algorithm - symplified)

    public void genDistances()
    {
        this.distances = new Distances(this);
        List<Cell2D> frontier = new List<Cell2D>();
        frontier.Add(this);
        genDistancesH(frontier, 0);
    }

    private void genDistancesH(List<Cell2D> frontier, int dist)
    {
        int newDist = dist + 1;
        List<Cell2D> newFrontier = new List<Cell2D>();
        foreach (Cell2D cell in frontier)
        {
            foreach (Cell2D link in cell.GetLinks())
            {
                if (!(this.distances.getCells().Contains(link)))
                {
                    this.distances.setDist(link, newDist);
                    newFrontier.Add(link);
                }
            }
        }
        if (!(newFrontier.Count == 0))
        {
            genDistancesH(newFrontier, newDist);
        }
    }

    public Distances getDistances() => this.distances;

    private string printKeys(List<Cell2D> keys)
    {
        string print = "";
        foreach (Cell2D key in keys)
        {
            print += "  " + key;
        }
        return print;
    }

    private bool allIsNull(List<Cell2D> list)
    {
        foreach (Cell2D cell in list)
        {
            if (!(cell is null))
            {
                return false;
            }
        }
        return true;
    }

}