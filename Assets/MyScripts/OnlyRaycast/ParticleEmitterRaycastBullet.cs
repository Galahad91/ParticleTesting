using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterRaycastBullet : MonoBehaviour
{
    public ParticleSystem Gun;
    public ParticleDecalPoolOnlyRaycast splatDecalPool;
    public ParticleSystem afterBurn;

    public bool startEmit = true;
    public bool canEmitCR = true;

    [HideInInspector] public ParticleSystem.Particle[] bullets;
    [HideInInspector] public float fireCD;

    public Gradient bulletColor;
    public float colorRange;
    public float timer = 0;
    public float rayLenght = 1;


    void Awake ()
    {
        fireCD = timer;
    }

   

    public void EmitAtLocation(Vector3 collision, Vector3 collisionNormal)
    {                       
        ParticleSystem.MainModule psMain = afterBurn.main;
        psMain.startColor = bulletColor.Evaluate(colorRange);
        afterBurn.transform.position = collision;
        afterBurn.transform.rotation = Quaternion.LookRotation(collisionNormal);

        afterBurn.Emit(1);
        Debug.Log("entro");
    }

    void Update ()
    {
        if (bullets == null || bullets.Length < Gun.main.maxParticles)
            bullets = new ParticleSystem.Particle[Gun.main.maxParticles];

        int numParticlesAlive = Gun.GetParticles(bullets);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            float currentSize = bullets[i].GetCurrentSize(Gun);
            RaycastHit hit;
            Debug.DrawRay(bullets[i].position, bullets[i].velocity, Color.blue);

            Debug.Log(bullets[i].GetCurrentSize(Gun));
            if (Physics.Raycast(bullets[i].position, bullets[i].velocity, out hit, rayLenght))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                  // EmitAtLocation(hit.point,hit.normal);
                   splatDecalPool.ParticleHit(hit.point, bulletColor, hit.normal, colorRange);
                   bullets[i].startLifetime = 0;
                }
            }
          
        }

        // Apply the particle changes to the particle system
        Gun.SetParticles(bullets, numParticlesAlive);

        if (fireCD > 0)
        {
            fireCD -= Time.deltaTime;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            ParticleSystem.MainModule psMain = Gun.main;
            psMain.startColor = bulletColor.Evaluate(colorRange);

            if (fireCD <= 0f)
            {
                psMain.gravityModifier = 0.2f;
                Gun.Emit(1);
                fireCD = timer;              
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            colorRange = Random.Range(0f, 1f);
        }
	}
}
