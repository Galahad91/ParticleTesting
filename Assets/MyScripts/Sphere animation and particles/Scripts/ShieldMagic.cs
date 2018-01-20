using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMagic : MagicStance
{
    ParticleSystem shield1;
    ParticleSystem shield2;
    ParticleSystem shield3;

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

            shield1 = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            shield2 = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
            shield3 = transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();
            impact = transform.GetChild(3).GetComponent<ParticleSystem>();

            isInitialized = true;
        }

        ChangeStartColor();
    }

    public override void ChangeStartColor()
    {
        ParticleSystem.MainModule psmain1 = shield1.main;
        psmain1.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(shield1);
        ParticleSystem.MainModule psmain2 = shield2.main;
        psmain2.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(shield2);
        ParticleSystem.MainModule psmain3 = shield3.main;
        psmain3.startColor = color.Evaluate(cRange);
        ChangeColorAllParticles(shield3);
    }

    public override void UseSkill()
    {
        if (state.currentStance == Stance.Default || state.currentStance == Stance.Defense)
        {
            Debug.Log("Attack");

        }
    }

	
}
