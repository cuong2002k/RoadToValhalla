using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TargetDropedItem : MonoBehaviour, IPointerClickHandler
{
    private GameObject _player;
    private ItemDropper _itemDropper;
    private UIManager _uIManager;

    private void Start()
    {
        _player = GameManager.Instance.Player;
        _itemDropper = _player.GetComponent<ItemDropper>();
        _uIManager = UIManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_uIManager.slotMove == null) return;
        int slotid = _uIManager.slotMove.GetSlotID();
        ItemStack itemDrop = _uIManager.slotMove.Inventory[slotid];
        int stackToDrop = _uIManager.slotMove.GetItemUI().GetStack();
        _itemDropper.DropItemSpawner(itemDrop, stackToDrop);

    }
}
