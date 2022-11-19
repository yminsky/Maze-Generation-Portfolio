using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface RandomMaze
{
    public Grid getMazePlan();
    public RenderMaze getRenderer();

    public RenderMaze getRenderer(Vector3 pos);
}