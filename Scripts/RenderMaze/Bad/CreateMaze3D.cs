using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze3D : MonoBehaviour, CreateMaze
{
    private int[,,] maze;
    private int width;
    private int length;
    private int height;
    private float unit = 10;
    public CreateMaze3D(int[,,] maze)
    {
        this.maze = maze;
        this.width = maze.GetLength(0);
        this.length = maze.GetLength(1);
        this.height = maze.GetLength(2);
    }

    public void drawMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (maze[i, j, k] == 1)
                    {
                        GameObject cube = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                        cube.transform.position = new Vector3(unit * j, unit * k, unit * i);
                        cube.transform.localScale = new Vector3(unit, unit, unit);
                    }
                }
            }
        }
    }
    public int getHeight()
    {
        return height;
    }
    public int getLength()
    {
        return length;
    }
    public int getWidth()
    {
        return width;
    }
    public int[,,] getMaze()
    {
        return maze;
    }
    public float getUnit()
    {
        return unit;
    }
    public void setUnit(float unit)
    {
        this.unit = unit;
    }
}
