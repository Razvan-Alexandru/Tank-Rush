using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{

    public float areaRadius;
    public float damageMultiplier = 1.5f;

    void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaRadius);
        foreach(Collider collider in colliders) {
            Enemy enemy = collider.transform.root.gameObject.GetComponent<Enemy>();
            if(collider.gameObject == gameObject) { continue; }
            if(enemy != null && !enemy.isBuffed) {
                enemy.ApplyBuff(damageMultiplier);
                enemy.isBuffed = true;
            }
        }
    }

    public void Disable() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaRadius);
        foreach(Collider collider in colliders) {
            Enemy enemy = collider.transform.root.gameObject.GetComponent<Enemy>();
            if(collider.gameObject == gameObject) { continue; }
            if(enemy != null && enemy.isBuffed) { 
                enemy.ApplyDebuff(damageMultiplier);
                enemy.isBuffed = false;
            }
        }
    }
}
