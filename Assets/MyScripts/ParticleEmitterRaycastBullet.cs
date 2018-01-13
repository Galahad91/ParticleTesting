using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterRaycastBullet : MonoBehaviour
{
    public ParticleSystem flameThrower;
    public ParticleSystem afterBurn;
    public ParticleDecalPool splatDecalPool;

    [HideInInspector] public ParticleSystem firstChild;
    [HideInInspector] public ParticleSystem secondChild;
    [HideInInspector] public ParticleSystem.Particle[] bullets;
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
        if (bullets == null || bullets.Length < secondChild.main.maxParticles)
            bullets = new ParticleSystem.Particle[secondChild.main.maxParticles];

        int numParticlesAlive = secondChild.GetParticles(bullets);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            RaycastHit hit;
            Debug.DrawRay(bullets[i].position, transform.forward, Color.blue);

            if (Physics.Raycast(bullets[i].position, transform.forward, out hit, rayLenght))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    EmitAtLocation(hit.point);
                }
            }
          
        }

        // Apply the particle changes to the particle system
        secondChild.SetParticles(bullets, numParticlesAlive);

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
