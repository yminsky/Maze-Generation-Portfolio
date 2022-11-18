using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float speed = 0.11f;
    void Start()
    {
        GameManager.player = gameObject;
        gameObject.transform.position = NPCMain.randomPos();
        NPCShoot.playerPos = gameObject.transform.position;
    }

    void Update()
    {
        float moveHorizontal = speed * Input.GetAxis("Horizontal");
        float moveVertical = speed * Input.GetAxis("Vertical");
        gameObject.transform.position += new Vector3(moveHorizontal, 0, moveVertical);
    }
}
