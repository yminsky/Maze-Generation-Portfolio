using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    const int len = 10;

    public static Grid2D smileyFace()
    {
        Grid2D grid = new Grid2D(len, len);
        Shaper2D shaper = new Shaper2D(grid);
        shaper.removeSquare(2, 6, 2);
        shaper.removeSquare(2, 6, 6);
        shaper.remove(3, 1);
        shaper.remove(3, 8);
        shaper.removeRowLine(2, 2, 1);
        shaper.removeRowLine(2, 2, 7);
        shaper.removeRowLine(6, 1, 2);
        return grid;
    }

    public static Grid2D zigzag()
    {
        Grid2D grid = new Grid2D(14, 10);
        Shaper2D shaper = new Shaper2D(grid);
        shaper.removeRowLine(8, 2, 0);
        shaper.removeRowLine(8, 3, 0);
        shaper.removeRowLine(8, 6, 2);
        shaper.removeRowLine(8, 7, 2);
        shaper.removeRowLine(8, 10, 0);
        shaper.removeRowLine(8, 11, 0);
        return grid;
    }

}
