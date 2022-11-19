using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell3D : Cell
{
    private int row;
    private int col;
    private int level;
    private bool active;
    private Dictionary<direction, Cell3D> neighbors;
    private List<Cell> links;
    private Distances distances;

    public Cell3D(int row, int col, int level)
    {
        this.row = row;
        this.col = col;
        this.level = level;
        this.active = true;
        this.links = new List<Cell>();
        this.neighbors = new Dictionary<direction, Cell3D>();
        InitializeNeighbors();
    }

    public int GetRow() => row;

    public int GetColumn() => col;

    public int GetLevel() => level;

    public bool IsActive() => active;

    public void SetStatus(bool active)
    {
        this.active = active;
    }

    public override string ToString()
    {
        return row.ToString() + ", " + col.ToString() + ", " + level.ToString();
    }

    public override bool Equals(object other)
    {
        if (other is Cell3D)
        {
            Cell3D cell = other as Cell3D;
            return this.row == cell.GetRow() &&
                   this.col == cell.GetColumn() &&
                   this.level == cell.GetLevel();
        }
        else return false;
    }

    public override int GetHashCode()
    {
        return row.GetHashCode() + col.GetHashCode() + level.GetHashCode();
    }

    //bidi: bidirectional
    public void Link(Cell3D cell)
    {
        LinkH(cell, true);
    }

    public void LinkH(Cell3D cell, bool bidi)
    {
        links.Add(cell);
        if (bidi)
        {
            cell.LinkH(this, false);
        }
    }

    public void Unlink(Cell3D cell)
    {
        UnlinkH(cell, true);
    }

    public void UnlinkH(Cell3D cell, bool bidi)
    {
        this.links.Remove(cell);
        if (bidi)
        {
            cell.UnlinkH(this, false);
        }
    }

    public List<Cell> GetLinks() => links;

    public bool IsLinked(Cell3D cell) => this.links.Contains(cell);

    private void InitializeNeighbors()
    {
        neighbors.Add(direction.North, null);
        neighbors.Add(direction.South, null);
        neighbors.Add(direction.East, null);
        neighbors.Add(direction.West, null);
        neighbors.Add(direction.Up, null);
        neighbors.Add(direction.Down, null);
    }
    public Cell3D GetNeighbor(direction dir) => neighbors[dir];

    public Dictionary<direction, Cell3D> GetNeighbors()
    {
        Dictionary<direction, Cell3D> dict = new Dictionary<direction, Cell3D>();
        foreach (direction key in this.neighbors.Keys)
        {
            Cell3D val = this.neighbors[key];
            if (!(val is null))
            {
                if (val.IsActive()) dict.Add(key, val);
            }
        }
        return dict;
    }

    public void SetNeighbor(direction dir, Cell3D neighbor)
    {
        neighbors[dir] = neighbor;
    }

    public direction GetRelation(Cell3D cell)
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
        if (cell.Equals(neighbors[direction.West]))
        {
            return direction.West;
        }
        if (cell.Equals(neighbors[direction.Up]))
        {
            return direction.Up;
        }
        else { return direction.Down; }
    }

    public enum direction
    {
        North,
        East,
        South,
        West,
        Up,
        Down
    }

    //finding solutions (Dijkstra's algorithm - symplified)

    public void GenDistances()
    {
        this.distances = new Distances(this);
        List<Cell3D> frontier = new List<Cell3D>();
        frontier.Add(this);
        GenDistancesH(frontier, 0);
    }

    public void GenDistancesH(List<Cell3D> frontier, int dist)
    {
        int newDist = dist + 1;
        List<Cell3D> newFrontier = new List<Cell3D>();
        foreach (Cell3D cell in frontier)
        {
            foreach (Cell3D link in cell.GetLinks())
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
            GenDistancesH(newFrontier, newDist);
        }
    }

    public Distances GetDistances() => this.distances;

}
