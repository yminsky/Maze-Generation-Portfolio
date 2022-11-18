using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{

    void Start()
    {
        Vector3 pos = NPCMain.randomPos();
        gameObject.transform.position = pos;
        //print("key placed");

    }
}
