﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPoolOnlyRaycast : MonoBehaviour
{
    public int maxDecals = 100;
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;

	void Start ()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
       
        for(int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData(); 
        }
	}

    public void ParticleHit(Vector3 collision, Gradient colorGradient, Vector3 collisionNormal, float colorRange)
    {
        SetParticleData(collision, colorGradient, collisionNormal, colorRange);
        DisplayParticles();
    }

    void SetParticleData(Vector3 collision, Gradient colorGradient, Vector3 collisionNormal, float colorRange)
    {
        if(particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = collision;
        Vector3 particleRotationEuler = Quaternion.LookRotation(collisionNormal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 300);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(colorRange);
        particleDecalDataIndex++;
    }

    void DisplayParticles()
    {
        for(int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
	
}
