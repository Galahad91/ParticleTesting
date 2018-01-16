﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpheres : MonoBehaviour
{
    public Animator anim;
    public float stance;
    public GameObject[] Fireballs;
    public int fireballCapacity;
    public GameObject fireball;
    public GameObject fireballDepot;
    public Gradient color;
    public float cRange = 0;

    [HideInInspector] public ParticleSystem.Particle[] bullets;

     ParticleSystem sphere1;
     ParticleSystem sphere2;
     ParticleSystem sphere3;

    void Start ()
    {
        sphere1 = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        sphere2 = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
        sphere3 = transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();

        ChangeStartColor(sphere1, sphere2, sphere3);

        Fireballs = new GameObject[fireballCapacity];
        for (int i = 0; i < fireballCapacity; i++)
        {
            Fireballs[i] = Instantiate(fireball, fireballDepot.transform);
            Fireballs[i].transform.position = Fireballs[i].transform.parent.position;
        }
	}
	
    void ChangeStartColor(ParticleSystem sphere1, ParticleSystem sphere2, ParticleSystem sphere3)
    {
        Debug.Log("ChangeStartColor");
        ParticleSystem.MainModule psmain1 = sphere1.main;
        psmain1.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sphere1);
        ParticleSystem.MainModule psmain2 = sphere2.main;
        psmain2.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sphere2);
        ParticleSystem.MainModule psmain3 = sphere3.main;
        psmain3.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sphere3);
    }

    void ChangeColorAllParticles(ParticleSystem sphere)
    {
        Debug.Log("ChangeColorAllParticles");
        if (bullets == null || bullets.Length < sphere.main.maxParticles)
            bullets = new ParticleSystem.Particle[sphere.main.maxParticles];

        int numParticlesAlive = sphere.GetParticles(bullets);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            float currentSize = bullets[i].GetCurrentSize(sphere);
           
            bullets[i].startColor = color.Evaluate(cRange);
        }

        // Apply the particle changes to the particle system
        sphere.SetParticles(bullets, numParticlesAlive);

    }

	void Update ()
    {


        if (Input.GetMouseButtonDown(1))
        {

            if (stance == 1)
            {
                stance = 0;
                cRange = 0;

                ChangeStartColor(sphere1,sphere2,sphere3);      
            }
            else
            {
                stance += 0.5f;
                cRange = stance;

                ChangeStartColor(sphere1, sphere2, sphere3);
            }
      
            anim.SetFloat("Stance", stance);

        }
        if(Input.GetMouseButtonDown(0))
        {
            if (stance == 0 || stance == 1)
            {
                for (int i = 0; i < fireballCapacity;)
                {
                    if (Fireballs[i].GetComponent<SphereBehaviour>().isActive)
                    {
                        i++;
                    }
                    else
                    {
                        Debug.Log("entro");
                        Fireballs[i].transform.position = transform.position + new Vector3(0, 1, 1);
                        Fireballs[i].GetComponent<Rigidbody>().isKinematic = false;
                        Fireballs[i].GetComponent<Rigidbody>().AddForce(transform.parent.forward * 2000);
                        Fireballs[i].GetComponent<SphereBehaviour>().isActive = true;
                        Fireballs[i].GetComponent<SphereBehaviour>().color = color;
                        Fireballs[i].GetComponent<SphereBehaviour>().cRange = cRange;

                        ParticleSystem.MainModule psmain = Fireballs[i].transform.GetChild(0).GetComponent<ParticleSystem>().main;
                        psmain.startColor = color.Evaluate(cRange);

                        ParticleSystem sphere = Fireballs[i].transform.GetChild(0).GetComponent<ParticleSystem>();
                        ChangeColorAllParticles(sphere);   
                        
                        break;
                    }
                }
            }           
        }
#region Inputs
        if (Input.GetMouseButton(0) && stance == 0.5f)
        {

            if (Input.GetKey(KeyCode.D))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((Vector3.up + transform.parent.right) * (1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.W))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((Vector3.up + transform.parent.forward) * (1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((Vector3.up + (-transform.parent.right)) * (1000 * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.S))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((Vector3.up + (-transform.parent.forward)) * (1000 * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((-Vector3.up) * (1000 * Time.deltaTime));
            }
            else
            {
                transform.parent.GetComponent<Rigidbody>().AddForce((Vector3.up) * (1000 * Time.deltaTime));
            }

        }
#endregion

    }
}