using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    public bool isActive = false;
    public float hitMarker = 2;
    public ParticleSystem explosion;
    public Rigidbody gravity;
    public Gradient color;
    public float cRange;
    float timer = 4;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(isActive)
        {
            //if(timer > 0)
            //{

            //}

            gravity.useGravity = true;
            RaycastHit hit;
            Debug.DrawRay(transform.position, gravity.velocity, Color.red);
           if( Physics.Raycast(transform.position, gravity.velocity, out hit, hitMarker))
           {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
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
}
