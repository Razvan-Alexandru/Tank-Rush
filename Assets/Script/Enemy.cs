using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType {
    TankTrap,
    Tower,
    Mortar,
    Tank
}

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public bool isBuffed = false;

    public int enemyScore;

    public HealthBar healthBar;

    void Awake() {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start() {
         currentHealth = maxHealth;
         healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public virtual void TakeDamage(float amount) {
        currentHealth -= amount;
        healthBar.UpdateHealth(currentHealth, maxHealth);
        if(currentHealth <= 0) {
            Die();
        }
    }

    public void ApplyBuff(float amount) {
        damage *= amount;
    }

    public void ApplyDebuff(float amount) {
        damage /= amount;
    }

    public virtual void Die() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddScore(enemyScore);
        FindObjectOfType<GameManagerScript>().RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
        TowerEnemy towerEnemy = GetComponent<TowerEnemy>();
        if(towerEnemy != null) {
            towerEnemy.Disable();
        }
    }
}
