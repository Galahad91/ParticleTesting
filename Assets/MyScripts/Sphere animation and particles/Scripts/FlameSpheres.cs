using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpheres : MagicStance
{


    public int magicUses;
    public GameObject bullet;
    public GameObject[] currentBulletList;
    ParticleSystem sphere1;
    ParticleSystem sphere2;
    ParticleSystem sphere3;

    void Awake ()
    {
        isInitialized = false;
        bulletDepot = GameManager.instance.magicDepot.gameObject;

        currentBulletList = new GameObject[magicUses];
        for (int i = 0; i < magicUses; i++)
        {
            currentBulletList[i] = Instantiate(bullet, bulletDepot.transform);
            currentBulletList[i].transform.position = currentBulletList[i].transform.parent.position;
        }
	}

    public override void InitializeMagic()
    {
        if (!isInitialized)
        {
            anim = GetComponent<Animator>();
            crosshairPos = CrossHairManager.instance.crosshairPos;
            state = GameManager.instance.player.GetComponent<PlayerState>();

            sphere1 = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            sphere2 = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
            sphere3 = transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();
            impact = transform.GetChild(3).GetComponent<ParticleSystem>();

            isInitialized = true;
        }
        ChangeStartColor();
    }

    public override void ChangeStartColor()
    {
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

    public override void UseSkill()
    {
        if (state.currentStance == Stance.Default || state.currentStance == Stance.Defense)
        {
            for (int i = 0; i < magicUses;)
            {
                if (currentBulletList[i].GetComponent<SphereBehaviour>().isActive)
                {
                    i++;
                }
                else
                {
                    Vector3 shootdir;
                    RaycastHit hit;

                    if (Physics.Raycast(crosshairPos.position, -crosshairPos.forward, out hit))
                        shootdir = (hit.point - currentWeapon.position).normalized;
                    else
                        shootdir = -crosshairPos.forward.normalized;

                    Debug.DrawRay(crosshairPos.position, -crosshairPos.forward);

                    currentBulletList[i].transform.position = currentWeapon.GetChild(0).position;
                    currentBulletList[i].transform.rotation = Quaternion.LookRotation(shootdir);
                    currentBulletList[i].GetComponent<Rigidbody>().isKinematic = false;
                    currentBulletList[i].GetComponent<Rigidbody>().AddForce(shootdir * 2000);
                    currentBulletList[i].GetComponent<SphereBehaviour>().isActive = true;
                    currentBulletList[i].GetComponent<SphereBehaviour>().color = color;
                    currentBulletList[i].GetComponent<SphereBehaviour>().cRange = cRange;

                    ParticleSystem.MainModule psmain = currentBulletList[i].transform.GetChild(0).GetComponent<ParticleSystem>().main;
                    psmain.startColor = color.Evaluate(cRange);

                    ParticleSystem sphere = currentBulletList[i].transform.GetChild(0).GetComponent<ParticleSystem>();
                    ChangeColorAllParticles(sphere);

                    break;
                }
            }

        }
    }

	
}
