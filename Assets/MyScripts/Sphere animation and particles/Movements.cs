using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public FlameSpheres stance;
    public ParticleSystem impact;
    public float rayLenght;
    public CameraScript camerMain;
    public Transform cameraTransform;
    public float health = 100;
    public float skillDmg = 20;
    public float heightCap = 50f;
    public float currentHeight;
    public float velocityCap = 20f;
    public Vector3 constantSpeed;
    public Vector3 constantHeight;


    [HideInInspector] public bool isFlying = false;
    [HideInInspector] public float velocity;
    [HideInInspector] public Rigidbody controller;

	// Use this for initialization
	void Awake ()
    {
        currentHeight = heightCap - transform.position.y;
        controller = GetComponent<Rigidbody>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camerMain = cameraTransform.GetComponent<CameraScript>();
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
            GameManager.instance.currentEnemyNumber--;
        }
    }


    void FixedUpdate ()
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

        transform.Rotate(new Vector3(0, camerMain.currentX, 0));
        //transform.rotation = Quaternion.LookRotation(cameraTransform.forward);

        currentHeight = transform.position.y;

        if ((stance.cap == 0 || stance.cap == 1) && !isFlying)
        {
            if (Input.GetKey(KeyCode.D))
            {
                constantSpeed = new Vector3(velocityCap, 0, 0);
                controller.AddForce(transform.right * (1000 * Time.deltaTime));
                if (controller.velocity.x > velocityCap)
                    controller.velocity = constantSpeed;
            }

            else if (Input.GetKey(KeyCode.W))
            {
                constantSpeed = new Vector3(0, 0, velocityCap);
                controller.AddForce(transform.forward * (1000 * Time.deltaTime));
                if (controller.velocity.z > velocityCap)
                    controller.velocity = constantSpeed;
            }

            else if (Input.GetKey(KeyCode.A))
            {
                constantSpeed = new Vector3(velocityCap, 0, 0);
                controller.AddForce(-transform.right * (1000 * Time.deltaTime));
                if (controller.velocity.x > velocityCap)
                    controller.velocity = constantSpeed;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                constantSpeed = new Vector3(0, 0, velocityCap);
                controller.AddForce(-transform.forward * (1000 * Time.deltaTime));
                if (controller.velocity.z > velocityCap)
                    controller.velocity = constantSpeed;
            }
        }

        if (stance.cap == 0.5f)
        {
            if (Input.GetKey(KeyCode.D))
            {
                constantSpeed = new Vector3(velocityCap, 0, 0);
                controller.AddForce(transform.right * (1000 * Time.deltaTime));
                if (controller.velocity.x > velocityCap)
                    controller.velocity = constantSpeed;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                constantSpeed = new Vector3(0, 0, velocityCap);
                controller.AddForce(transform.forward * (1000 * Time.deltaTime));
                if (controller.velocity.z > velocityCap)
                    controller.velocity = constantSpeed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                constantSpeed = new Vector3(velocityCap, 0, 0);
                controller.AddForce(-transform.right * (1000 * Time.deltaTime));
                if (controller.velocity.x > velocityCap)
                    controller.velocity = constantSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                constantSpeed = new Vector3(0, 0, velocityCap);
                controller.AddForce(-transform.forward * (1000 * Time.deltaTime));
                if (controller.velocity.z > velocityCap)
                    controller.velocity = constantSpeed;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                constantHeight = constantSpeed + new Vector3(0, velocityCap, 0);
                controller.AddForce((-Vector3.up) * (1000 * Time.deltaTime));
                if (controller.velocity.y > velocityCap)
                    controller.velocity = constantHeight;
            }
            else if (Input.GetMouseButton(0) && currentHeight < heightCap)
            {
                constantHeight = constantSpeed + new Vector3(0, velocityCap, 0);
                controller.AddForce((Vector3.up) * (1000 * Time.deltaTime));
                if (controller.velocity.y > velocityCap)
                    controller.velocity = constantHeight;
            }
        }
        
        #endregion
    }
}
