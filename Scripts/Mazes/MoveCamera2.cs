using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera2 : MonoBehaviour
{
    const float speed = 0.2f;
    const int resetTime = 500;
    private const float MouseSpeed = 3f;
    private float mousePosX = 0f;
    private float mousePosY = 0f;
    private int resetTick = -1000;

    void Start()
    {
    }

    private void Update()
    {
        bool reset = Input.GetKey(KeyCode.R);
        if (reset) { resetTick = Manager.tick; }

        float xmovement = speed * Input.GetAxis("Horizontal");
        float zmovement = speed * Input.GetAxis("Vertical");
        bool up = Input.GetKey(KeyCode.Q);
        bool down = Input.GetKey(KeyCode.Z);
        float ymovement = 0;
        if (up) ymovement = speed;
        if (down) ymovement = -speed;
        // gameObject.transform.Translate(new Vector3(0, ymovement, 0), Space.World); // not going up?!
        gameObject.transform.Translate(new Vector3(xmovement, ymovement, zmovement), Space.World);

        Vector3 currentAngles = transform.eulerAngles;

        if (Manager.tick - resetTick > resetTime)
        {
            mousePosX -= MouseSpeed * Input.GetAxis("Mouse Y");
            mousePosY += MouseSpeed * Input.GetAxis("Mouse X");
            currentAngles.x = mousePosX;
            currentAngles.y = mousePosY;
        }

        transform.eulerAngles = currentAngles;
    }
}