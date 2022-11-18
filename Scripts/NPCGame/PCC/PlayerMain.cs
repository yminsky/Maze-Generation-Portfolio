using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    int health;
    int healthcap = 5;
    int keysCollected;
    bool doorMade;
    bool foundKey = false;
    static float whiteness = 0;
    Material mat;

    void Start()
    {
        health = healthcap;
        keysCollected = 0;
        doorMade = false;
        whiteness = 0;
        mat = gameObject.GetComponent<Renderer>().material;
        setColor();
    }

    void Update()
    {
        if (health == 0)
        {
            GameManager.gameWon = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Endgame");
        }
        if (keysCollected == GameManager.keyCount && !doorMade)
        {
            GameManager.makeDoor();
            doorMade = true;
        }
        if (GameManager.tick % 4 == 0 && foundKey)
        {
            keysCollected++;
            foundKey = false;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("NPCProjectile") && !NPCShoot.hitPlayer)
        {
            NPCShoot.hitPlayer = true;
            health--;
            whiteness += (1.0f / healthcap);
            setColor();
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            foundKey = true;
            //print("keys collected: " + keysCollected);
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.CompareTag("Door"))
        {
            GameManager.gameWon = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Endgame");
        }
    }

    private void setColor()
    {
        Color color = new Color(whiteness, 1, 1, 1);
        mat.SetColor("_Color", color);
    }
}
