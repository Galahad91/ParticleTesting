using System.Collections;
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
    public float cRange;
     ParticleSystem sphere1;
     ParticleSystem sphere2;
     ParticleSystem sphere3;

    void Start ()
    {
        sphere1 = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        sphere2 = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
        sphere3 = transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();
        Fireballs = new GameObject[fireballCapacity];
        for (int i = 0; i < fireballCapacity; i++)
        {
            Fireballs[i] = Instantiate(fireball, fireballDepot.transform);
            Fireballs[i].transform.position = Fireballs[i].transform.parent.position;
        }
	}
	
	void Update ()
    {
		if(Input.GetMouseButtonDown(1))
        {

            if (stance == 1)
            {
                stance = 0;
                cRange = 0;
                ParticleSystem.MainModule psmain1 = sphere1.main;
                psmain1.startColor = color.Evaluate(cRange);
                ParticleSystem.MainModule psmain2 = sphere2.main;
                psmain2.startColor = color.Evaluate(cRange);
                ParticleSystem.MainModule psmain3 = sphere3.main;
                psmain3.startColor = color.Evaluate(cRange);
                sphere1.Emit(1);
                sphere2.Emit(1);
                sphere3.Emit(1);
            }
            else
            {
                stance += 0.5f;
                cRange = stance;
                ParticleSystem.MainModule psmain1 = sphere1.main;
                psmain1.startColor = color.Evaluate(cRange);
                ParticleSystem.MainModule psmain2 = sphere2.main;
                psmain2.startColor = color.Evaluate(cRange);
                ParticleSystem.MainModule psmain3 = sphere3.main;
                psmain3.startColor = color.Evaluate(cRange);
                sphere1.Emit(1);
                sphere2.Emit(1);
                sphere3.Emit(1);
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

                        Fireballs[i].transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
                        Fireballs[i].transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Emit(1);
                        Fireballs[i].transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Emit(1);
                        break;
                    }
                }
            }           
        }
        if (Input.GetMouseButton(0) && stance == 0.5f)
        {

             transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up *(1000*Time.deltaTime));

        }

    }
}
