using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMagic : MagicStance
{
    Transform orbit1;
    Transform orbit2;
    ParticleSystem sword1;
    ParticleSystem sword2;
    ParticleSystem sword3;
    ParticleSystem sword4;
    ParticleSystem sword5;
    ParticleSystem sword6;


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
            orbit1 = transform.GetChild(0);
            orbit2 = transform.GetChild(1);

            sword1 = orbit1.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            sword2 = orbit1.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
            sword3 = orbit1.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();

            impact = transform.GetChild(2).GetComponent<ParticleSystem>();

            isInitialized = true;
        }
        ChangeStartColor();
    }

    public override void ChangeStartColor()
    {
        ParticleSystem.MainModule psmain1 = sword1.main;
        psmain1.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sword1);
        ParticleSystem.MainModule psmain2 = sword2.main;
        psmain2.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sword2);
        ParticleSystem.MainModule psmain3 = sword3.main;
        psmain3.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(sword3);
    }

    public override void UseSkill()
    {
        if (state.currentStance == Stance.Default || state.currentStance == Stance.Defense)
        {
            Debug.Log("Attack");

        }
    }

	
}
