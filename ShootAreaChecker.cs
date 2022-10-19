using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAreaChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (player == null) return;
        player.HoldTrigger = true;
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (player == null) return;
        player.HoldTrigger = false;
    }
}
