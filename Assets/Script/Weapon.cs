using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public enum WeaponType { Cannon, Missile, MachineGun }
    public WeaponType weaponType;

    public float bulletSpeed;
    public float bulletDamage;
    public float cooldownTime;

    public GameObject bullet;
    public Transform firePoint;

    [Header("Machine Gun Specific")]
    public float firingRate;

    public int overheatPerShot = 4;
    public int overheatThreshold = 100;
    public int maxRoundsBeforeOverheat = 30;
    public float overheatCooldown = 5f;

    [Header("Missile Specific")]
    public int roundsPerBurst = 4;             
    public float burstDelay = .5f;
    public float areaDamageRadius = 5f;
    
    [HideInInspector] public bool isCoolingDown = false;
    [HideInInspector] public int shotCounter = 0;
    public float cooldownRemaining;

    public bool isReadyToFire() {
        return !isCoolingDown;
    }
}
