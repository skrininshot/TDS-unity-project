using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAreaChecker : MonoBehaviour, IPointerClickHandler
{
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        player.Shooting();
    }
}
