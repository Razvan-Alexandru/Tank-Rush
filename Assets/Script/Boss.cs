using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float baseSpeed = .5f;
    public float bossSpeed;
    public float damage = 10f;
    public float damageInterval = 1f;

    private Transform player;
    private float damageTimer = 0f;
    private float playerMaxSpeed;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMaxSpeed = player.GetComponent<TankMovement>().maxSpeed;
    }


    void Update() {
        baseSpeed = Mathf.Min(baseSpeed, playerMaxSpeed / 2);
        float playerSpeed = player.GetComponent<TankMovement>().currentSpeed;
        bossSpeed = (playerMaxSpeed - Mathf.Abs(playerSpeed)) / 2 + baseSpeed;

        transform.position = Vector3.MoveTowards(transform.position, player.position, bossSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, player.position) < 5f) {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval) {
                player.GetComponent<Player>().TakeDamage(damage);
                damageTimer = 0f;
            }
        }
    }
}
