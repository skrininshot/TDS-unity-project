using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private int treesCount = 100;
    [SerializeField] private GameObject treePrefab;
    private void Awake()
    {
        //-50;50 : 50;-50
        for (int i = 0; i < treesCount; i++)
        {
            GameObject newTree = Instantiate(treePrefab, transform);
            newTree.transform.position = new Vector3 (Random.Range(-50, 50), Random.Range(50, -50), -5);
            newTree.transform.rotation =  Quaternion.Euler(0, 0, Random.Range(0f, 1f));
        }
    }
}
