using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBursttest : MonoBehaviour
{
    ParticleSystem particle;
    int min;
    int max;
    // Use this for initialization
    void Start ()
    {
        particle = GetComponent<ParticleSystem>();
        var emission = particle.emission;
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
        emission.GetBursts(bursts);
        max = bursts[0].maxCount;
        min = bursts[0].minCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            particle.Emit(Random.Range(min, max));
        }
	}
}
