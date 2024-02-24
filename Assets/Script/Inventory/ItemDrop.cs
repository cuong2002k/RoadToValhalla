using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDrop : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance.slotMove == null) return;
        UIManager.Instance.DropItemSlotUI();

    }
}
