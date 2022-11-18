using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreateMaze2D : MonoBehaviour, CreateMaze
{
    private int[,] maze;
    private int height;
    private int length;
    private float unit = 10;

    public CreateMaze2D(int[,] maze)
    {
        this.maze = maze;
        this.height = maze.GetLength(0);
        this.length = maze.GetLength(1);
    }

    public float getUnit()
    {
        return unit;
    }
    public void setUnit(float unit)
    {
        this.unit = unit;
    }
    public int getHeight()
    {
        return height;
    }

    public int getLength()
    {
        return length;
    }

    public int[,] getMazePlan()
    {
        return maze;
    }

    public void drawMaze()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (maze[i, j] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                    cube.transform.position = new Vector3(unit * j, 0, unit * i);
                    cube.transform.localScale = new Vector3(unit, unit, unit);
                }
            }
        }
    }
}
