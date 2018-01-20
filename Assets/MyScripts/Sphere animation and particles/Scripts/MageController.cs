using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MageController : MonoBehaviour
{
    public float impactRayLenght;
    public float health = 100;
    public float heightCap = 50f;
    public float velocityCap = 20f;
    public float fireCD = 1;

    public PlayerState state;
    public Transform weaponHolder;
    [HideInInspector] public MagicStance currentMagic;

    [HideInInspector] public float timer;
    [HideInInspector] public float currentHeight;
    [HideInInspector] public bool isFlying = false;
    [HideInInspector] public float velocity;

    [HideInInspector] public CameraScript cameraMain;
    [HideInInspector] public Transform cameraTransform;
    [HideInInspector] public Transform weapon;
    [HideInInspector] public Vector3 constantHeight;
    [HideInInspector] public Vector3 constantSpeed;
    [HideInInspector] public Rigidbody controller;

	// Use this for initialization
	void Awake ()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        currentMagic = GameObject.FindGameObjectWithTag("CurrentMagic").GetComponent<MagicStance>();
        currentHeight = heightCap - transform.position.y;
        controller = GetComponent<Rigidbody>();
        cameraMain = cameraTransform.GetComponent<CameraScript>();
    }

    void Update()
    {


        velocity = controller.velocity.magnitude;
        RaycastHit hit;
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        if(Physics.Raycast(transform.position,-Vector3.up, out hit, impactRayLenght))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Default")) 
            {
                if(velocity > 15 && isFlying)
                {
                    ParticleSystem.MainModule psmain = currentMagic.impact.GetComponent<ParticleSystem>().main;
                    psmain.startColor = currentMagic.color.Evaluate(currentMagic.cRange);
                    currentMagic.impact.transform.position = transform.position;

                    currentMagic.impact.Emit(1);
                }

                isFlying = false;
            }
        }
        else
        {
                isFlying = true;
        }

        #region Skills

        if (Input.GetMouseButtonDown(0))
        {
            currentMagic.UseSkill();
        }

#endregion

        #region Movements

        Vector3 dir = cameraTransform.forward;
        dir.y = 0;

        transform.rotation = Quaternion.LookRotation(dir);
        weapon.DOLookAt(CrossHairManager.instance.crosshairPos.position,0.2f);

        currentHeight = transform.position.y;

        if (state.currentStance == Stance.Default || state.currentStance == Stance.Defense)
        {
            if (!isFlying)
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

            

        }

        if (state.currentStance == Stance.Mobility)
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
