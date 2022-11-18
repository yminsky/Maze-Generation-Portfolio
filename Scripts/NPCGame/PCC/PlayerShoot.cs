using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    static int tick = 0;
    int coolDown = 60;
    int shootTick = -1000;
    Vector3 mousePos;
    public static bool hitNPC = false;
    Camera main;
    public static bool shooting = false;

    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        tick = GameManager.tick;
        mousePos = main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, CamFollow.height));
        //print(mousePos);
        if (Input.GetKeyDown(KeyCode.Space) && tick - shootTick > coolDown)
        {
            this.shootTick = tick;
            //print(mousePos);
            StartCoroutine(shoot(new Vector3(mousePos.x, 0.5f, mousePos.z)));
        }
    }

    private IEnumerator shoot(Vector3 endPoint)
    {
        shooting = true;
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider col = sphere.AddComponent<SphereCollider>();
        sphere.tag = "PlayerProjectile";
        sphere.transform.position = gameObject.transform.position;
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.25f, 0, 1));
        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = false;
        //print("pre rotation: " + sphere.transform.rotation);
        sphere.transform.rotation = Quaternion.LookRotation(endPoint - sphere.transform.position, Vector3.up);
        sphere.transform.Translate(new Vector3(0, 0, 0.5f), Space.Self);
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        //print("rotated: " + sphere.transform.rotation);
        while (!(hitNPC || tick - shootTick > 50))
        {
            //print("during while: " + sphere.transform.rotation);
            //sphere.transform.Translate(new Vector3(0, 0, 0.3f), Space.Self);
            //rb.velocity = new Vector3(0, 0, 0.3f);
            rb.AddForce(sphere.transform.TransformDirection(0, 0, 0.7f), ForceMode.VelocityChange);
            //print("dir: " + sphere.transform.TransformDirection(0, 0, 1));
            yield return new WaitForEndOfFrame();
            //print("end of while: " + sphere.transform.rotation);
        }
        Destroy(sphere);
        shooting = false;
        hitNPC = false;
        //print("shooting");
    }

}