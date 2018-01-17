using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public FlameSpheres stance;
    public ParticleSystem impact;
    public float rayLenght;
    public Transform camerMain;
    public float health = 100;
    public float skillDmg = 20;
    public float heightCap = 50f;
    public float currentHeight;
    public float velocityCap = 20f;


    [HideInInspector] public bool isFlying = false;
    [HideInInspector] public float velocity;
    [HideInInspector] public Rigidbody controller;

	// Use this for initialization
	void Awake ()
    {
        currentHeight = heightCap - transform.position.y;
        controller = GetComponent<Rigidbody>();
        camerMain = transform.Find("CameraPin");//.GetChild(0).GetChild(0); 
    }

    public void DealDamage(GameObject enemy, float dmg)
    {
        Debug.Log("entro");
        if (enemy.GetComponent<Enemy>().health > dmg)
        {
            enemy.GetComponent<Enemy>().health -= dmg;
        }
        else
        {
            Destroy(enemy);
        }
    }


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
                    ParticleSystem.MainModule psmain = impact.GetComponent<ParticleSystem>().main;
                    psmain.startColor = stance.color.Evaluate(stance.cRange);
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
        #region Movements

        transform.rotation = Quaternion.LookRotation(camerMain.forward);


        if (stance.cap != 0.5f && !isFlying)
        {          
            if (velocity < velocityCap)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if(controller.velocity.z < velocityCap)
                       controller.AddForce(transform.forward * velocityCap);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (controller.velocity.x > -velocityCap)
                        controller.AddForce(-transform.right * velocityCap);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (controller.velocity.z > -velocityCap)
                        controller.AddForce(-transform.forward * velocityCap);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (controller.velocity.x < velocityCap)
                        controller.AddForce(transform.right * velocityCap);
                }
            }
        }
        #endregion

        #region Flying
        if (stance.cap == 0.5f)
        {
            //Mathf.Clamp(currentHeight, 0, heightCap);

            //currentHeight = heightCap - transform.position.y;

            if (Input.GetKey(KeyCode.D))
            {
                if (controller.velocity.x < velocityCap)
                    controller.AddForce((transform.right) * ( 1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.W))
            {
                //if (controller.velocity.z < velocityCap)
                    controller.AddForce(transform.forward * (1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                //if (controller.velocity.x < -velocityCap)
                    controller.AddForce(-transform.right * (1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.S))
            {
               // if (controller.velocity.z < -velocityCap)
                    controller.AddForce(-transform.forward * (1000 * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                //if (controller.velocity.y < -velocityCap)
                    controller.AddForce((-Vector3.up) * (1000 * Time.deltaTime));
            }
            else if (Input.GetMouseButton(0))
            {
                //if (controller.velocity.y < velocityCap)
                    controller.AddForce((Vector3.up) * (1000 * Time.deltaTime));            
            }

        }
        #endregion

    }
}
