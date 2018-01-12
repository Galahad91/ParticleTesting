using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
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



    void Awake ()
    {
        fireCD = timer;

        firstChild = flameThrower.transform.GetChild(0).GetComponent<ParticleSystem>();
        secondChild = flameThrower.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

   

    public void EmitAtLocation(ParticleCollisionEvent collision)
    {

        ParticleSystem.MainModule psMain = afterBurn.main;
        psMain.startColor = flameColor.Evaluate(colorRange);
        afterBurn.transform.position = collision.intersection;
        afterBurn.transform.rotation = Quaternion.LookRotation(collision.normal);
        
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
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            colorRange = Random.Range(0f, 1f);
        }
	}
}
