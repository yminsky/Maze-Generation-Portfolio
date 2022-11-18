using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMain : MonoBehaviour
{
    int health;
    const int healthcap = 1;
    Collider col;
    //static float opacity = 1;
    static float whiteness = 0;
    Material mat;

    void Start()
    {
        health = healthcap;
        //print("getting renderer");
        mat = gameObject.GetComponent<Renderer>().material;
        //print("gonna change color");
        setColor();
        //mat.shader = Shader.Find("Transparent/Diffuse"); //uncomment to use opacity
        //print("changed color");
    }
    void Update()
    {
        if (health == 0)
        {
            respawn();
            //opacity = 1;
            whiteness = 0;
            setColor();
            health = healthcap;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        //print("it's a bullet: " + other.gameObject.CompareTag("PlayerProjectile")
        //    + ", not already shot: " + !PlayerShoot.hitNPC);
        if (other.gameObject.CompareTag("PlayerProjectile") && !PlayerShoot.hitNPC)
        {
            //print("both");
            PlayerShoot.hitNPC = true;
            health--;
            //opacity -= 0.2f;
            whiteness += (1.0f / healthcap);
            setColor();
            //print("beenShot, health: " + health);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void respawn()
    {
        gameObject.transform.position = randomPos();
    }

    public static Vector3 randomPos()
    {
        int rows = GameManager.rows;
        int cols = GameManager.cols;
        int row = Random.Range(0, rows);
        int col = Random.Range(0, cols);
        return mazeToCoord(row, col);
    }

    public static Vector3 mazeToCoord(int row, int col)
    {
        int unit = GameManager.unit;
        float x = col * unit;
        float z = (row + 0.5f) * unit; //test this individually (without respawn)
        return new Vector3(x, 0.5f, z);
    }

    private void setColor()
    {
        Color color = new Color(whiteness, 1, whiteness, 1); //change 1 to opacity if using opacity
        mat.SetColor("_Color", color);
    }
}
