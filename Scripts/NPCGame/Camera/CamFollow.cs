using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    GameObject player;
    GameObject npc;
    public const float height = 12;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npc = GameObject.FindGameObjectWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0, height, 0); //follow player
        //gameObject.transform.position = npc.transform.position + new Vector3(0, 10, 0); //follow npc
    }
}
