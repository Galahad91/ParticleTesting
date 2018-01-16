using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform cameraPin;
    Vector3 rot;
    float mouseX;

	void Start ()
    {
        cameraPin = transform.parent.parent;
	}
	

	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime *5;
        rot = new Vector3(0, mouseX, 0);

        cameraPin.transform.Rotate( rot);
        
	}
}
