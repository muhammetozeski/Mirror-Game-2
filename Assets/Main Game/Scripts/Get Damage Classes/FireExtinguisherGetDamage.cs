using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FireExtinguisherGetDamage : MonoBehaviour, IDamageable
{
    [EButton]
    void apply()
    {
        GetComponent<Renderer>().material.SetColor("_EmissiveColor", EmissiveColor * EmissionMultiplier);
    }

    [SerializeField] Renderer renderer;
    [SerializeField] Color EmissiveColor;
    private float _emissionMultiplier;
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

    IDamageable._OnDeath onDeath;
    IDamageable._OnDeath IDamageable.OnDeath { 
        get { return onDeath; }
        set { onDeath = value; }
    }

    [SerializeField] private float Health = 100;
    float IDamageable.Health { get { return Health; } set { Health = value; } }

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
            MainTools.ShakeYZ(transform, TransformPosition);
            #endregion
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
        if (!renderer)
            renderer = GetComponent<Renderer>();
        TransformPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(ShotTime < Time.time + StartCooling && EmissionMultiplier >= 0)
        {
            EmissionMultiplier -= EmissionMultiplierDecreaser;
            renderer.material.SetColor("_EmissiveColor", EmissiveColor * EmissionMultiplier);
            transform.position = TransformPosition; //back to the first position
        }
    }
}
