using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicStance : MonoBehaviour
{

     public Gradient color;
     public GameObject bulletDepot;
     public PlayerState state;
     public Animator anim;
     public float cRange = 0;
     public Transform currentWeapon;
     public Transform crosshairPos;
     public ParticleSystem.Particle[] bullets;
     public ParticleSystem impact;
     public bool isInitialized;

    public abstract void ChangeStartColor();

    public abstract void InitializeMagic();

    public void ChangeColorAllParticles(ParticleSystem stanceMagic)
    {
        if (bullets == null || bullets.Length < stanceMagic.main.maxParticles)
            bullets = new ParticleSystem.Particle[stanceMagic.main.maxParticles];

        int numParticlesAlive = stanceMagic.GetParticles(bullets);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            float currentSize = bullets[i].GetCurrentSize(stanceMagic);
           
            bullets[i].startColor = color.Evaluate(cRange);
        }

        // Apply the particle changes to the particle system
        stanceMagic.SetParticles(bullets, numParticlesAlive);

    }

    public abstract void UseSkill();
  

	
}
