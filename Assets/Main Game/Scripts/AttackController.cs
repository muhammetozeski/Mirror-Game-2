using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField, Range(-30, -0.1f)] float Damage;
    [SerializeField] LaserMaker laser;


}
