using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public FlameSpheres stance;
    public float velocity;
    public ParticleSystem impact;
    public bool isFlying = false;
    public float rayLenght;
    //CharacterController controller;
    Rigidbody controller;

	// Use this for initialization
	void Start ()
    {
        //controller = GetComponent<CharacterController>();
        controller = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        velocity = controller.velocity.magnitude;
        RaycastHit hit;
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        if(Physics.Raycast(transform.position,-Vector3.up, out hit, rayLenght))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Default")) 
            {
                if(velocity > 15 && isFlying)
                {
                    impact.transform.position = transform.position;
                    impact.Emit(1);
                }

                isFlying = false;
            }
        }
            else
            {
                isFlying = true;
            }

		if(stance.stance != 0.5f && !isFlying)
        {
            if(Input.GetKey(KeyCode.W))
            {
                controller.AddForce(transform.forward * 20);
            }
            if (Input.GetKey(KeyCode.A))
            {
                controller.AddForce(-transform.right * 20);
            }
            if (Input.GetKey(KeyCode.S))
            {
                controller.AddForce(-transform.forward * 20);
            }
            if (Input.GetKey(KeyCode.D))
            {
                controller.AddForce(transform.right * 20);
            }
        }
	}
}
