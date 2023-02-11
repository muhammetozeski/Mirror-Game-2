using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SimpleExplosionController : MonoBehaviour
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

    void Start()
    {
        OPFireVFX = GameManager.Instance.OPFireVFX;
        MainTools.CheckNull(particleSystem);
    }
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();
        ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, particleCollisionEvents);
        foreach (var collision in particleCollisionEvents)
        {
            Transform Fire = OPFireVFX.GetPool().transform;
            Fire.gameObject.SetActive(false); //otherwise particles leave footprints
            Fire.position = collision.intersection;
            //Fire.rotation = Quaternion.FromToRotation(Vector3.up, collision.normal);
            Fire.gameObject.SetActive(true);
        }
    }

}
