using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    public GameObject player;
    public bool isActive = false;
    public float hitMarker = 2;
    public ParticleSystem explosion;
    public Rigidbody gravity;
    public Gradient color;
    public float cRange;
    float timer;
    public float cd = 4;
    public float skillDmg = 40;

    void Start ()
    {
        player = GameManager.instance.player;
	}
	
	void Update ()
    {
		if(isActive)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                gravity.useGravity = false;
                gravity.isKinematic = true;
                transform.position = transform.parent.position;
                timer = cd;
                isActive = false;
            }

           // gravity.useGravity = true;
            RaycastHit hit;
            Debug.DrawRay(transform.position, gravity.velocity, Color.red);
           if( Physics.Raycast(transform.position, gravity.velocity, out hit, hitMarker))
           {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    GameManager.instance.DealDamage(hit.transform.gameObject, skillDmg);
                    StartCoroutine(CrossHairManager.instance.EnemyHit());
                }
                ParticleSystem.MainModule psmain = explosion.main;
                psmain.startColor = color.Evaluate(cRange);
                explosion.transform.position = hit.point;
                explosion.transform.rotation = Quaternion.LookRotation(hit.normal);
                explosion.Emit(1);
                gravity.useGravity = false;
                gravity.isKinematic = true;
                transform.position = transform.parent.position;
                isActive = false;
           }
        }
	}
}
