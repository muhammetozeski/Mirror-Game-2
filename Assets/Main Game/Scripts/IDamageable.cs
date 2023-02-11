using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    public float Health
    {
        get;
        protected set;
    }
    /// <summary>
    /// enter a positive value to increase health and run increase health animation
    /// <br></br>
    /// enter a negative value to decrease health and see decreasing health animation
    /// <br></br>
    /// enter 0 or don't enter anything to only get health as float
    /// </summary>
    /// <returns>Health of the object</returns>
    public float IncreaseHealth(float Health, Vector3? HitLocation = null);

    public void PushEffect(float impulse, Vector3 ToDirection);

    delegate void _OnDeath();
    public _OnDeath OnDeath { protected get; set; }
}
