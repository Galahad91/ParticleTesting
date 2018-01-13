using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterRaycast : MonoBehaviour
{
    public ParticleSystem flameThrower;
    public ParticleSystem afterBurn;
    public ParticleDecalPool splatDecalPool;

    [HideInInspector] public ParticleSystem firstChild;
    [HideInInspector] public ParticleSystem secondChild;
    [HideInInspector] public float fireCD;

    public Gradient flameColor;
    public float colorRange;
    public float timer = 0;
    public float rayLenght = 2;


    void Awake ()
    {
        fireCD = timer;

        firstChild = flameThrower.transform.GetChild(0).GetComponent<ParticleSystem>();
        secondChild = flameThrower.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

   

    public void EmitAtLocation(Vector3 collision)
    {
        ParticleSystem.MainModule psMain = afterBurn.main;
        psMain.startColor = flameColor.Evaluate(colorRange);
        afterBurn.transform.position = collision;
        afterBurn.transform.rotation = Quaternion.LookRotation(collision);
        
        afterBurn.Emit(1);
        afterBurn.transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
        afterBurn.transform.GetChild(1).GetComponent<ParticleSystem>().Emit(1);
    }

    void Update ()
    {
        if (fireCD > 0)
        {
            fireCD -= Time.deltaTime;
        }
        
        if (Input.GetMouseButton(0))
        {
            ParticleSystem.MainModule psMain = flameThrower.main;
            psMain.startColor = flameColor.Evaluate(colorRange);
            flameThrower.Emit(1);
            firstChild.Emit(1);

            if (fireCD <= 0f)
            {
                secondChild.Emit(1);
                fireCD = timer;

                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward, Color.blue);

               if( Physics.Raycast(transform.position,transform.forward, out hit, rayLenght))
               {
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
                    {
                        EmitAtLocation(hit.point);
                    }
               }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            colorRange = Random.Range(0f, 1f);
        }
	}
}
