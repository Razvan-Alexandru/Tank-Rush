
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Transform player;
    public GameObject boss;
    public List<GameObject> enemyPrefabs;

    public int initialEnemyCount = 5;
    public int maxEnemyCount = 100;
    private List<GameObject> enemies = new List<GameObject>();

    private float spawnTimer = 0f;
    private float spawnIntervals = 5f;

    public float spawnRadiusFromPlayer = 35f;

    void Start() {
        Vector3 playerSpawn = GetRandomSpawnPoint();
        player.position = playerSpawn;

        Vector3 bossSpawn = GetRandomSpawnPoint();
        Instantiate(boss, bossSpawn, Quaternion.identity);
        
        for(int i = 0; i < initialEnemyCount; i++) {
            SpawnEnemy();
        }
    }
 
    void Update() {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnIntervals && enemies.Count < maxEnemyCount) {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if(player.GetComponent<Player>().timer % 15 < Time.deltaTime) {
            IncreaseDifficulty();
        }
    }

    Vector3 GetRandomSpawnPoint() {

        Vector3 spawnPoint;
        bool isValidSpawnPoint = false;

        do {
            float x = Random.Range(-150, 150);
            float z = Random.Range(-150, 150);
            spawnPoint = new Vector3(x, 0, z);

            isValidSpawnPoint = !Physics.CheckSphere(spawnPoint, 2f, LayerMask.GetMask("Obstacle"));
        } while (!isValidSpawnPoint);

        return spawnPoint;
    }

    Vector3 GetRandomSpawnPointAroundPlayer() {
        Vector3 spawnPoint;
        bool isValidSpawnPoint = false;

        do {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(5f, spawnRadiusFromPlayer);
            spawnPoint = new Vector3(player.position.x + Mathf.Cos(angle) * distance, 0f, player.position.z + Mathf.Sin(angle) * distance);

            isValidSpawnPoint = !Physics.CheckSphere(spawnPoint, 2f, LayerMask.GetMask("Obstacle"));
        } while (!isValidSpawnPoint);

        return spawnPoint;
    }

    void SpawnEnemy() {
        GameObject newEnemy = SelectEnemyType();
        enemies.Add(newEnemy);
    }

    GameObject SelectEnemyType() {
        float proximityWeight = 0.8f;

        GameObject enemyToSpawn;

        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        enemyToSpawn = enemyPrefabs[enemyIndex];
        EnemyType enemyType = enemyToSpawn.GetComponent<Enemy>().enemyType;

        if(enemyType == EnemyType.TankTrap) {
            if(Random.value < proximityWeight) {
                return SpawnNearExistingEnemy(EnemyType.TankTrap, enemyToSpawn);
            }
        }
        else if(enemyType == EnemyType.Mortar || enemyType == EnemyType.Tank) {
            if(Random.value < proximityWeight) {
                return SpawnNearExistingEnemy(EnemyType.Tower, enemyToSpawn);
            }
        }
        else if(enemyType == EnemyType.Tower) {
            if(Random.value < proximityWeight) {
                return SpawnFarFromEnemies(EnemyType.Tower, enemyToSpawn);
            }
        }
        

        return Instantiate(enemyToSpawn, GetRandomSpawnPointAroundPlayer(), Quaternion.identity);
    }

    GameObject SpawnNearExistingEnemy(EnemyType enemyType, GameObject prefab) {

        GameObject[] existingEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> matchingEnemy = new List<GameObject>();

        foreach(GameObject enemy in existingEnemy) {
            if(enemy!= null && enemy.GetComponent<Enemy>().enemyType == enemyType) {
                matchingEnemy.Add(enemy);
            }
        }

        if(matchingEnemy.Count > 0) {
            GameObject targetEnemy = matchingEnemy[Random.Range(0, matchingEnemy.Count)];
            Vector3 spawnPosition = targetEnemy.transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            if(Vector3.Distance(spawnPosition, player.position)  <= spawnRadiusFromPlayer && 
                    !Physics.CheckSphere(spawnPosition, 2f, LayerMask.GetMask("Obstacle"))) {
                return Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
        return Instantiate(prefab, GetRandomSpawnPointAroundPlayer(), Quaternion.identity);
    }

    GameObject SpawnFarFromEnemies(EnemyType enemyType, GameObject prefab) {

        GameObject[] existingEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 spawnPosition = GetRandomSpawnPoint();
        bool isValidPosition = true;

        foreach(GameObject enemy in existingEnemy) {
            if(enemy!= null && enemy.GetComponent<Enemy>().enemyType == enemyType) {
                if(Vector3.Distance(spawnPosition, enemy.transform.position) < 20f) {
                    isValidPosition = false;
                    break;
                }
            }
        }

        if(isValidPosition && Vector3.Distance(spawnPosition, player.position) <= spawnRadiusFromPlayer + 15f && 
                !Physics.CheckSphere(spawnPosition, 2f, LayerMask.GetMask("Obstacles"))) {
            return Instantiate(prefab, spawnPosition, Quaternion.identity);
        }

        return Instantiate(prefab, GetRandomSpawnPointAroundPlayer(), Quaternion.identity);
    }

    void IncreaseDifficulty() {
        int random = Random.Range(0, 4);
        switch(random) {
            case 0:
                spawnIntervals = Mathf.Max(1f, spawnIntervals - 0.5f);
                break;
            case 1:
                foreach(GameObject prefab in enemyPrefabs) {
                    if(prefab != null) {
                        prefab.GetComponent<Enemy>().maxHealth += 10;
                    }
                }
                break;
            case 2:
                foreach(GameObject enemy in enemies) {
                    if(enemy != null) {
                        enemy.GetComponent<Enemy>().damage += 10;
                    }
                }
                break;
            case 3:
                boss.GetComponent<Boss>().baseSpeed += 0.1f;
                boss.GetComponent<Boss>().damage += 0.2f;
                break;
        }
    }

    public void RemoveEnemyFromList(GameObject enemy) {
        if(enemies.Contains(enemy)) {
            enemies.Remove(enemy);
        }
    }
}
