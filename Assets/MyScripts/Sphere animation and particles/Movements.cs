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

    [HideInInspector] public bool isFlying = false;
    [HideInInspector] public float velocity;
    [HideInInspector] public Rigidbody controller;

	// Use this for initialization
	void Awake ()
    {
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

        if(stance.cap != 0.5f && !isFlying)
        {
            if (velocity < 20)
            {
                if (Input.GetKey(KeyCode.W))
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
        #endregion
    }
}
