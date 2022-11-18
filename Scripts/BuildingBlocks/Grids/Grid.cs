using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Grid
{
    public void genActiveCells();

    public ArrayList getActiveCells();

    public void configureCells();

    public Cell randomCell();

    //public object getCells();

    public int getRows();

    public int getCols();

    public IEnumerable getCells();
}
