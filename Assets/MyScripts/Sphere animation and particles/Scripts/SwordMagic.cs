using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMagic : MagicStance
{
    public List<ParticleSystem> particles;

    void Awake ()
    {
        isInitialized = false;
    }

    public override void InitializeMagic()
    {
        if (!isInitialized)
        {
            anim = GetComponent<Animator>();
            crosshairPos = CrossHairManager.instance.crosshairPos;
            state = GameManager.instance.player.GetComponent<PlayerState>();

            impact = transform.GetChild(2).GetComponent<ParticleSystem>();

            isInitialized = true;
        }
        ChangeStartColor();
    }

    public override void ChangeStartColor()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            ParticleSystem.MainModule psmain1 = particles[i].main;
            psmain1.startColor = color.Evaluate(cRange);
            ChangeColorAllParticles(particles[i]);
        }      
    }

    public override void UseSkill()
    {
        if (state.currentStance == Stance.Default || state.currentStance == Stance.Defense)
        {
            Debug.Log("Attack");

        }
    }

	
}
