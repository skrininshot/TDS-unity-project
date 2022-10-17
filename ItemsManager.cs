using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Sprite[] ItemsSprites;
    public enum ItemsTypes { Nothing, Medkit, BigMedkit, Ammo, BigAmmo, Money, BigMoney };
    public static int[] Prices = { 0, 100, 200, 100, 200, 50, 100};
}
