using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    public ParticleEmitter particleController;
    //public ParticleEmitterRaycast particleController;

    [SerializeField]public List<ParticleCollisionEvent> collisionEvents;

    void Start ()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        particleController = transform.parent.GetComponent<ParticleEmitter>();
        //particleController = transform.parent.GetComponent<ParticleEmitterRaycast>();
    }

    private void OnParticleCollision(GameObject other)
    {

        ParticlePhysicsExtensions.GetCollisionEvents(particleController.secondChild, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            particleController.splatDecalPool.ParticleHit(collisionEvents[i], particleController.flameColor);
            particleController.EmitAtLocation(collisionEvents[i]);
            Debug.Log("Hit");
        }
    }
   
}
