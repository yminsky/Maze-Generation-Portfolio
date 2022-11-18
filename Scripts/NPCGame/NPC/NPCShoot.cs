using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCShoot : MonoBehaviour
{
    static Cell2D npcCell;
    static Cell2D playerCell;
    int dist;
    static int tick = 0;
    int coolDown = 300;
    int shotTick = -1000;
    FindPath path;
    public static bool canSeePlayer = false;
    public static bool hitPlayer = false;
    public static Vector3 playerPos;
    static Vector3 lastPlayerPos;
    static Vector3 playerVel;
    Vector3 playerPosPred;
    int predTicks;

    void Update()
    {
        path = MoveNPC.path;
        tick = GameManager.tick;
        lastPlayerPos = playerPos;
        playerPos = GameManager.player.transform.position;
        playerVel = playerPos - lastPlayerPos;
        if (tick > 0) //is this neccissary or did I just add it for testing?
        {
            //print("tick: " + tick);
            try
            {
                npcCell = path.GetStartpoint();
                //playerCell = path.GetEndpoint();
                dist = path.GetDistances().GetDist(npcCell);
                setPredTicks(dist);
                playerPosPred = playerPos + playerVel * predTicks;
                Cell2D playerCellPred = MoveNPC.GetMazePos(playerPosPred);
                bool sameRow = dist == Mathf.Abs(npcCell.GetColumn() - playerCellPred.GetColumn());
                bool sameCol = dist == Mathf.Abs(npcCell.GetRow() - playerCellPred.GetRow());
                canSeePlayer = sameRow || sameCol;
                //print("canSeePlayer: " + canSeePlayer);
                if (canSeePlayer && tick - shotTick > coolDown && dist < 10)
                {
                    hitPlayer = false; //switch out when you make reaction to hit
                    this.shotTick = tick;
                    StartCoroutine(shoot(playerPosPred));
                    //print("shoot");
                }
            }
            catch (NullReferenceException) { }
            //  print("tick: " + tick + ", npc cell: " + npcCell + ", player cell" + playerCell);

        }
    }

    private IEnumerator shoot(Vector3 endPoint)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider col = sphere.AddComponent<SphereCollider>();
        sphere.tag = "NPCProjectile";
        sphere.transform.position = gameObject.transform.position;
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.25f, 0, 1));
        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = false;
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.rotation = Quaternion.LookRotation(endPoint - sphere.transform.position, Vector3.up);
        sphere.transform.Translate(new Vector3(0, 0, 0.5f), Space.Self);
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        while (!(hitPlayer || tick - shotTick > 50))
        {
            //sphere.transform.Translate(new Vector3(0, 0, 0.3f), Space.Self);
            rb.AddForce(sphere.transform.TransformDirection(0, 0, 0.7f), ForceMode.VelocityChange);
            yield return new WaitForEndOfFrame();
        }
        Destroy(sphere);
    }

    private void setPredTicks(int dist)
    {
        int predTicks = dist * 500;
    }
}
