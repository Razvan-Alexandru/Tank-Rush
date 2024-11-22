using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy
{
    public GameObject tankProjectile;
    public Transform turret;
    public Transform firePoint;
    public float fireDelay = 5f;
    public float initialVelocity;
    public float maxRange;
    public GameObject hitParticles;

    private Transform player;
    private float timeToFire;

    void Start() {
        GetComponent<TankEnemy>().damage = GetComponent<Enemy>().damage;
        player= GameObject.FindGameObjectWithTag("Player").transform;
        timeToFire = Time.time + fireDelay;
    }

    void Update() {
        if(Time.time >= timeToFire) {
            FireAtPlayer();
            timeToFire = Time.time + fireDelay;
        }
        RotateTowardsPlayer();
    }

    void FireAtPlayer() {
        if (player == null) return;


        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= maxRange) {
            Vector3 direction = (player.transform.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(tankProjectile, firePoint.position, Quaternion.LookRotation(-firePoint.forward));
        
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            
            rb.velocity = -firePoint.forward * initialVelocity;
            rb.useGravity = true;

            projectile.AddComponent<Projectile>();
            Projectile projectileComponent = projectile.GetComponent<Projectile>();

            projectileComponent.damage = GetComponent<Enemy>().damage;
            projectileComponent.hitParticles = hitParticles;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - turret.position;
        directionToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer);
        turret.rotation = targetRotation;
    }
}
