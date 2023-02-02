using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnvironments : MonoBehaviour, IDamageable
{
    float Health = 100;


    IDamageable._OnDeath _onDeath;
    IDamageable._OnDeath IDamageable.onDeath { get { return _onDeath; } set => _onDeath = value; }

    float IDamageable.IncreaseHealth(float health = 0)
    {
        Health += health;
        return Health;
    }

    void IDamageable.PushEffect(float impulse, Vector3 ToDirection)
    {
        throw new NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
