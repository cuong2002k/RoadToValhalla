using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TargetDropedItem : MonoBehaviour, IPointerClickHandler
{
    private GameObject _player;
    private ItemDropper _itemDropper;
    private UI_DragDropManager _dragDropManager;

    private void Start()
    {
        _player = GameManager.Instance.Player;
        _itemDropper = _player.GetComponent<ItemDropper>();
        _dragDropManager = PlayerUIManager.Instance.DragDropManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_dragDropManager.slotMove == null) return;
        int slotid = _dragDropManager.slotMove.GetSlotID();
        ItemStack itemDrop = _dragDropManager.slotMove.Inventory[slotid];
        int stackToDrop = _dragDropManager.slotMove.GetItemUI().GetStack();
        _itemDropper.DropItemSpawner(itemDrop, stackToDrop);

    }
}
