using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SimpleExplosionSystemController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("After [LifeTime - desiredBurningDurationBeforeDeath] seconds, a fire will be instantiated.")]
    [SerializeField] float desiredBurningDurationBeforeDeath;

    [Header("Instances")]
    [SerializeField] ParticleSystem particleSystem;

    /// <summary>
    /// Fires vfx objects as a pool to be instantiated when Debris spread around
    /// </summary>
    ObjectPoolingSystem OPFireVFX;

    List<Particle> particles;
    int particleCount;

    [Tooltip("Debug purpose")]
    int isParticlesNull;

    void Start()
    {
        OPFireVFX = GameManager.Instance.OPFireVFX;
        MainTools.CheckNull(particleSystem);
        particles = new List<Particle>(particleSystem.main.maxParticles);
    }

    private void LateUpdate()
    {
        isParticlesNull = particles.Count;
        if(particleSystem.main.startLifetimeMultiplier <= desiredBurningDurationBeforeDeath)
        {
            particleCount = particleSystem.GetParticles(particles.ToArray());
            /*for (int i = 0; i < particleCount; i++)
            {
                Transform Fire = OPFireVFX.GetPool().transform;
                Fire.position = particleSystem.transform.TransformPoint(particles[i].position);
                Fire.gameObject.SetActive(true);
            }*/
        }
    }

}
