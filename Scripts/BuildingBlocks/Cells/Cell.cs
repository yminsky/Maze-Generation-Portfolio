using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Cell
{
    public int GetRow();

    public int GetColumn();

    public bool IsActive();

    public List<Cell> GetLinks();
}
