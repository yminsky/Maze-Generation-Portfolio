using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface CreateMaze
{
    public int getHeight();
    public int getLength();
    public float getUnit();
    public void setUnit(float unit);
    public void drawMaze();
}
