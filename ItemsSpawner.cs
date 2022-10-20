using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    void Start()
    {
        SpawnItems();
    }
    public void SpawnItems()
    {
        int[] countProbability = { 1, 1, 1, 1, 2, 2, 2, 3, 3 };
        int randomCount = Random.Range(1, countProbability.Length);

        for (int i = 0; i < countProbability[randomCount]; i++)
        {
            Item newItem = Instantiate(itemPrefab, FindObjectOfType<ItemsManager>().transform).GetComponent<Item>();
            Vector3 randomPosition = Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * (Random.Range(0, 20) / 10f);
            Vector3 playerPosition = transform.position + randomPosition;
            newItem.transform.position = new Vector3(playerPosition.x, playerPosition.y, -0.2f);
        }
    }
}
