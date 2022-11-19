using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RenderMaze
{
    public void drawMaze();

    public Grid getGrid();

    public float getUnit();

    public void setUnit(float unit);
}
