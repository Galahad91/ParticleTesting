using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public ParticleSystem skill;
    public GameObject player;
    public float skillCD = 4;
    public float skillTimer;
    public float rayLenght = 1;
    public float aggroRange = 30;
    public float dmg = 10;
    public float health = 100;
    public float pathTimer;
    public float pathCD = 2;

    [HideInInspector] public ParticleSystem.Particle[] bullets;
    [HideInInspector] public NavMeshAgent agent;

    void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
    private void Start()
    {
        player = GameManager.instance.player;
        agent.SetDestination(player.transform.position);
    }

	void Update ()
    {
        if(pathTimer > 0)
        {
            pathTimer -= Time.deltaTime;
        }
        else
        {
            agent.SetDestination(player.transform.position);
            pathTimer = pathCD;
        }

		if(Vector3.Distance(transform.position, player.transform.position)<aggroRange)
        {
            transform.LookAt(player.transform);

            if(skillTimer <= 0)
            {
                skill.Emit(1);
                skillTimer = skillCD;
            }
            else
            {
                skillTimer -= Time.deltaTime;
            }

           #region Particles Raycast

            if (bullets == null || bullets.Length < skill.main.maxParticles)
                bullets = new ParticleSystem.Particle[skill.main.maxParticles];

            int numParticlesAlive = skill.GetParticles(bullets);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                float currentSize = bullets[i].GetCurrentSize(skill);
                RaycastHit hit;
                Debug.DrawRay(bullets[i].position, bullets[i].velocity, Color.blue);

                if (Physics.Raycast(bullets[i].position, bullets[i].velocity, out hit, rayLenght))
                {

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        GameManager.instance.DealDamage(player, dmg);
                        bullets[i].startLifetime = 0;
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
                    {
                        //boom
                         bullets[i].startLifetime = 0;
                    }


                }

            }
            // Apply the particle changes to the particle system
            skill.SetParticles(bullets, numParticlesAlive);           
#endregion


        }
	}
}
