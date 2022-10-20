using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime = 45f;
    [SerializeField] private float maxSpawnTime = 60f;
    [SerializeField] private Enemy enemyPrefab;
    private Enemy currentEnemy;
    private Player player;
    private Building building;
    private bool timeToSpawn = false;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        building = FindObjectOfType<Building>();
    }

    public void StartTimer()
    {
        StartCoroutine(SpawnTimer());
    }
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.5f);
        if (FindObjectsOfType<Enemy>().Length > 0) yield break;
        if (!timeToSpawn)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
        timeToSpawn = false;
        SpawnEnemy();
        building.Close();
    }
    private void SpawnEnemy()
    {
        Vector3 spawnPosition = player.transform.position + Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * 10;
        currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.transform.position = new Vector3(
            Mathf.Clamp(spawnPosition.x, -35, 35),
            Mathf.Clamp(spawnPosition.x, -35, 35),
            -2f
            );
    }
    public void StopTimer()
    {
        StopCoroutine(SpawnTimer());
        timeToSpawn = true;
    }
}
