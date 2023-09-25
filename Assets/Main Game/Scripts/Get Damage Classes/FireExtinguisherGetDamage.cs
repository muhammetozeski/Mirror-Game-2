using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FireExtinguisherGetDamage : MonoBehaviour, IDamageable
{
    [EButton]
    void apply()
    {
        GetComponent<Renderer>().material.SetColor("_EmissiveColor", EmissiveColor * EmissionMultiplier);
    }
    [SerializeField] Collider collider;
    [SerializeField] AudioSource ExplosionSound;
    [SerializeField] GameObject ExplosionParticul;
    [SerializeField] Renderer renderer;
    [SerializeField] Color EmissiveColor;
    private float _emissionMultiplier = 1;
    [SerializeField] float EmissionMultiplier { 
        get { return _emissionMultiplier; }
        set { _emissionMultiplier = value < 0 ? 0 : value; }
    }
    [SerializeField] float EmissionMultiplierIncreaser;
    [SerializeField] float EmissionMultiplierDecreaser;

    [Tooltip("Starts cooling after x seconds")]
    [SerializeField] float StartCooling;

    float ShotTime = 0;

    Vector3 TransformPosition;

    

    [SerializeField] private float Health = 100;
    float IDamageable.Health { get { return Health; } set { Health = value; } }

    static void __empty__() { }
    Action onDeath = __empty__;
    Action IDamageable.OnDeath
    {
        get { return onDeath; }
        set { onDeath = value; }
    }

    //TODO: create particul effect according to "hit location"
    public float IncreaseHealth(float Health, Vector3? HitLocation = null)
    {
        if(Health < 0 && this.Health > 0)
        {
            print("decreasing health");
            #region Change color
            EmissionMultiplier += EmissionMultiplierIncreaser;
            renderer.material.SetColor("_EmissiveColor", EmissiveColor * EmissionMultiplier);
            this.Health += Health;
            ShotTime = Time.time;
            #endregion

            #region Shake
            MainTools.ShakeYZ(transform, TransformPosition,0.01f,1000);
            #endregion
        }
        if(this.Health <= 0)
        {
            onDeath();
        }
        return this.Health;
    }

    public void PushEffect(float impulse, Vector3 ToDirection)
    {
        //no push for now
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        if (!renderer)
            renderer = GetComponent<Renderer>();
        TransformPosition = transform.position;
        
        onDeath += () => {
            ExplosionParticul.SetActive(true);
            renderer.enabled = false;
            ExplosionSound.Play();
            collider.enabled = false;
        };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ShotTime < Time.time + StartCooling && EmissionMultiplier >= 0)
        {
            EmissionMultiplier -= EmissionMultiplierDecreaser;
            renderer.material.SetColor("_EmissiveColor", EmissiveColor * EmissionMultiplier);
        }
    }
}
