using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Sprite[] ItemsSprites;
    private ItemsSpawner[] spawners;
    public enum ItemsTypes { Nothing, Medkit, BigMedkit, Ammo, BigAmmo, Money, BigMoney };
    public static int[] Prices = { 0, 100, 300, 100, 300, 50, 150 };
    public static string[] ItemsNames = 
    {
        "Nothing",
        "Medkit",
        "Big medkit",
        "Ammo",
        "Lots of ammo",
        "Money",
        "Lots of money"
    };
    public static string[] ItemsDescriptions =
    {
        "nothing",
        "+30 health points",
        "+100 health points",
        "+1 ammo clip",
        "+3 ammo clip",
        $"+money",
        $"+lots of money"
    };
    private void Start()
    {
        spawners = FindObjectsOfType<ItemsSpawner>();
    }
    public void SpawnItems()
    {
        if (FindObjectsOfType<Item>().Length > 30) return;
        bool spawned = false;
        Vector3 playerPosition = FindObjectOfType<Player>().transform.position;
        while (!spawned)
        {
            int index = Random.Range(1, spawners.Length);
            if (Vector3.Distance(spawners[index].transform.position, playerPosition) < 5f) break;
            spawners[index].SpawnItems();
            spawned = true;
        } 
    }
}