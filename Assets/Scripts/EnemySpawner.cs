using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // the enemy object/prefab
    public float spawnRate = 2f; // time between spawns
    public bool enableRandomSpawnRate = false; // True to enable random respawn rate
    public float minSpawnRate = 1f, maxSpawnRate = 3f; // The random spawn interval between min max

    private void Start()
    {
        gameObject.SetActive(false); // Deactivate this spawner by default
    }

    /// <summary>
    /// Spawn enemy function, spawns random enemy out of array of enemy prefabs
    /// </summary>
    private void SpawnEnemy()
    {
        // if no enemy prefab, abort
        if (enemyPrefabs.Length == 0) return;

        // Pick random enemy prefab
        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn Position
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);

        // Spawn function
        Instantiate(randomEnemy, spawnPosition, Quaternion.identity);

        // Set the next invoke time, based on if random spawn rate is enabled or not
        float nextSpawnTime = enableRandomSpawnRate ? Random.Range(minSpawnRate, maxSpawnRate) : spawnRate;
        Invoke(nameof(SpawnEnemy), nextSpawnTime);
    }

    /// <summary>
    /// Activates the spawner
    /// </summary>
    public void ActivateSpawner()
    {
        if (!gameObject.activeSelf){
            gameObject.SetActive(true);
        }

        Invoke(nameof(SpawnEnemy), spawnRate);
    }
}
