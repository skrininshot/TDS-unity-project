using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime = 15f;
    [SerializeField] private float maxSpawnTime = 20f;
    [SerializeField] private Enemy enemyPrefab;
    private Player player;
    private Building building;
    private int spawnTime;
    public int spawnCounter = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        building = FindObjectOfType<Building>();
    }

    private IEnumerator Timer()
    {
        if (spawnCounter == 0)
        {
            spawnTime = (int)Random.Range(minSpawnTime, maxSpawnTime);
            spawnCounter = spawnTime;
        }
        
        while (spawnCounter > 0)
        {
            yield return new WaitForSeconds(1);
            spawnCounter--;
        }
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (player == null) return;
        Vector3 spawnPosition = player.transform.position + Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * 5;
        Enemy currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.transform.position = new Vector3(
            Mathf.Clamp(spawnPosition.x, -35, 35),
            Mathf.Clamp(spawnPosition.x, -35, 35),
            -2f
            );

        building.Close();
    }

    public void StartTimer()
    {
        StopTimer();
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }
}
