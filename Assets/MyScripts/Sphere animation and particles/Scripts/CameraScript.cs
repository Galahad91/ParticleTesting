using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform cameraPin;
    Vector3 rot;
    public float currentX;
    float currentY;
    public float yAngleMin = -40;
    public float yAngleMax = 70;
    public float distance;
    public float rotationSpeed = 1;

    protected Vector3 dir = new Vector3();
    protected Quaternion rotation = new Quaternion();


	void Update ()
    {
        // camera movement by axis
        currentX += Input.GetAxis("Mouse X") * rotationSpeed; ;
        currentY -= Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);

        //camera management of the bound to the player, movement, rotation and look direction
        dir.Set(0, 0, -distance);
        rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = cameraPin.position + rotation * dir;
        transform.LookAt(cameraPin.position);
     
    }
}
