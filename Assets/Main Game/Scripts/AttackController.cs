using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] LaserMaker laser;
    private void Start()
    {
        laser.OnLaserHitDamageable = () => {
            print("Attacking!");
            return Damage; 
        };    
    }

}
