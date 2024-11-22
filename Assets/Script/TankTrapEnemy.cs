using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTrapEnemy : Enemy
{

    void Start() {
        GetComponent<TankTrapEnemy>().damage = GetComponent<Enemy>().damage;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            DealDamage(player);
        }
    }

    public void DealDamage(Player player) {
        player.TakeDamage(GetComponent<Enemy>().damage);
    }
}
