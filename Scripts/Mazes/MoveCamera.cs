using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    const float speed = 0.2f;
    float k = 0.3f;
    const float z = 20;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        print("width: " + Screen.width);
    }
    void Update()
    {
        float xmovement = speed * Input.GetAxis("Horizontal");
        float zmovement = speed * Input.GetAxis("Vertical");
        bool up = Input.GetKey(KeyCode.Q);
        bool down = Input.GetKey(KeyCode.Z);
        float ymovement = 0;
        if (up) ymovement = speed;
        if (down) ymovement = -speed;
        gameObject.transform.position += new Vector3(xmovement, ymovement, zmovement);
        rotate();
    }

    private void turn()
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);
        Vector3 point = hit.point;
        Vector3 worldMousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookPos = new Vector3(worldMousePoint.x, 2, worldMousePoint.y);
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.5f);
    }

    public void rotate()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 xy = mouse - center;
        if (Mathf.Abs(xy.x) > 20) gameObject.transform.rotation *= Quaternion.AngleAxis(Mathf.Atan(xy.x / z) * k, Vector3.up);
        if (Mathf.Abs(xy.y) > 20) gameObject.transform.rotation *= Quaternion.AngleAxis(-Mathf.Atan(xy.y / z) * k, Vector3.right);
    }



}

