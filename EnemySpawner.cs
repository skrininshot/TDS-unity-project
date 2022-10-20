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
        if (currentEnemy != null)  yield break;
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        Vector3 spawnPosition = player.transform.position + Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * 15;
        currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.transform.position = new Vector3(
            Mathf.Clamp(spawnPosition.x, -35,35),
            Mathf.Clamp(spawnPosition.x, -35,35),
            -2f
            );
        building.Close();
        Debug.Log("Enemy spawned");
    }
    public void StopTimer()
    {
        StopCoroutine(SpawnTimer());
    }
}
