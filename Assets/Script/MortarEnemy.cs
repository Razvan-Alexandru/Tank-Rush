using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEnemy : Enemy
{
    public GameObject mortarProjectile;
    public Transform mortarTurret;
    public float fireDelay = 5f;
    public Transform firePoint;
    public float initialVelocity;

    public float minRange = 10f;
    public float maxRange = 50f;
    public float minLaunchAngle = 45f;

    private Transform player;
    private float timeToFire;
    public GameObject hitParticles;

    void Start() {
        GetComponent<MortarEnemy>().damage = GetComponent<Enemy>().damage;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeToFire = Time.time + fireDelay;
    }

    void Update() {
        if(Time.time >= timeToFire) {
            FireAtPlayer();
            timeToFire = Time.time + fireDelay;
        }
        RotateTowardsPlayer();
    }

    void FireAtPlayer()
    {
        if (player == null) return;


        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= maxRange && distanceToPlayer >= minRange) {
            GameObject projectile = Instantiate(mortarProjectile, firePoint.position, Quaternion.identity);
        
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            Vector3 launchDirection = CalculateDirection(firePoint.position, player.transform.position, initialVelocity);
            rb.velocity = launchDirection;
            rb.useGravity = true;

            projectile.AddComponent<Projectile>();
            Projectile projectileComponent = projectile.GetComponent<Projectile>();

            projectileComponent.isAreaDamage = true;
            projectileComponent.damage = GetComponent<Enemy>().damage;
            projectileComponent.hitParticles = hitParticles;
        }
    }

    Vector3 CalculateDirection(Vector3 mortarPos, Vector3 playerPos, float initialVelocity) {
        Vector3 direction = playerPos - mortarPos;
        float horizontalDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float verticalDistance = direction.y;

        // Calculate the time to reach the target based on fixed projectile speed
        float time = horizontalDistance / initialVelocity;

        // Calculate horizontal and vertical components of the velocity
        Vector3 horizontalVelocity = new Vector3(direction.x, 0, direction.z).normalized * initialVelocity;
        float verticalVelocity = (verticalDistance / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        // Return the combined velocity
        return horizontalVelocity + Vector3.up * verticalVelocity;
    }

    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - mortarTurret.position;
        directionToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        mortarTurret.rotation = Quaternion.Euler(-45f, targetRotation.eulerAngles.y, 0);
    }
}
