using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public static int screenWidth;
    public static int screenHeight;
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        //print("width: " + screenWidth + ", height: " + screenHeight);
    }

    void Update()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    public static void TopCenter(GameObject g)
    {
        float x = screenWidth / 2f;
        float y = 5f / 6f * screenHeight;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void DeadCenter(GameObject g)
    {
        float x = screenWidth / 2f;
        float y = screenHeight / 2f;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void Above(GameObject g, GameObject other)
    {
        float x = other.transform.position.x;
        float y = other.transform.position.y + screenHeight / 8f;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void Below(GameObject g, GameObject other)
    {
        float x = other.transform.position.x;
        float y = other.transform.position.y - screenHeight / 8f;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void toRightOf(GameObject g, GameObject other)
    {
        float x = other.transform.position.x + 200 * g.transform.localScale.x;
        float y = other.transform.position.y;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void toLeftOf(GameObject g, GameObject other)
    {
        float x = other.transform.position.x - 200 * g.transform.localScale.x;
        float y = other.transform.position.y;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void BottomRight(GameObject g)
    {
        float gap = 2;
        float x = screenWidth - gap - 65 * g.transform.localScale.x;
        float y = gap + 15 * g.transform.localScale.y;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
        Resize(g);
    }

    public static void BottomLeft(GameObject g)
    {
        Resize(g);
        float gap = 2;
        float x = gap + 65 * g.transform.localScale.x;
        float y = gap + 15 * g.transform.localScale.y;
        float z = g.transform.position.z;
        g.transform.position = new Vector3(x, y, z);
    }

    public static void Resize(GameObject g)
    {
        g.transform.localScale = new Vector3(screenWidth * 1.0f / 642, screenHeight * 1.0f / 252, 1);
    }
}
