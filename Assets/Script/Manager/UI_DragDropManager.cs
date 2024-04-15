using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UI_DragDropManager : MonoBehaviour
{
    public static UI_DragDropManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private Transform canvas;

    #region Inventory Handler
    [HideInInspector] public SlotUI slotMove;
    [HideInInspector] public Image iconDrag;
    [HideInInspector] public TextMeshProUGUI stackDrag;
    [HideInInspector] public static bool iDragHandler = false;

    #endregion

    private void Start()
    {
        canvas = this.transform;
    }


    private void Update()
    {
        if (iDragHandler)
        {
            MoveSlotItem(Input.mousePosition, stackDrag.gameObject);
            MoveSlotItem(Input.mousePosition, iconDrag.gameObject);
        }
    }

    public void OnClickHandler(SlotUI slot, PointerEventData eventData)
    {
        // slot click is not empty
        if (slot.IsEmpty()) return;

        if (!iDragHandler)
        {
            iDragHandler = true;
            slotMove = slot;
            if (InputManager.Instance.ShiftInput)
            {
                int stackToSplit = slotMove.GetItemUI().GetStack();
                stackToSplit = stackToSplit > 1 ? stackToSplit / 2 : 1;
                ItemStack itemToSplit = slotMove.GetItemUI().SplitStack(stackToSplit);
                slotMove.SetItemUI(itemToSplit);
            }
            iconDrag = Instantiate(slotMove.GetItemIcon(), canvas.transform);
            stackDrag = Instantiate(slotMove.GetStackText(), canvas.transform);
            stackDrag.text = slotMove.GetItemUI().GetStack().ToString();

        }
    }

    public void OnDropHandler(int fromslotID, int toSlotID, InventoryModel fromInventory, InventoryModel toInventory)
    {

        ItemStack fromSlot = fromInventory[fromslotID];
        ItemStack toSlot = toInventory[toSlotID];

        if (fromslotID == toSlotID && fromInventory == toInventory)
        {
            SwapSlot(fromSlot, toSlot);
        }
        // slot drop is not empty
        else if (!toSlot.IsEmpty())
        {
            // two slot contains equals item
            if (fromSlot.GetItem().Equals(toSlot.GetItem()))
            {
                // can combine
                if (toSlot.GetStackAvailable() >= fromSlot.GetStack())
                {
                    int stackRemaing = toSlot.AddStack(fromSlot.GetStack());
                    fromSlot.SetStack(stackRemaing);

                }
                else if (fromSlot.GetStack() == this.slotMove.GetItemUI().GetStack())
                {
                    SwapSlot(fromSlot, toSlot);

                }
            }
            else
            {
                if (fromSlot.GetStack() == this.slotMove.GetItemUI().GetStack())
                {
                    SwapSlot(fromSlot, toSlot);
                }
            }
        }
        // slot drop is empty
        else
        {

            if (fromSlot.GetStack() > this.slotMove.GetItemUI().GetStack())
            {
                toSlot.SetItemStack(slotMove.GetItemUI());
                fromSlot.DecreaseStack(slotMove.GetItemUI().GetStack());

            }
            else
            {
                SwapSlot(fromSlot, toSlot);

            }

        }

        fromInventory.Invoke();
        toInventory.Invoke();
        RestartMouseSlot();
    }

    public void RestartMouseSlot()
    {
        if (!iDragHandler) return;
        iDragHandler = false;
        slotMove = null;
        Destroy(iconDrag.gameObject);
        Destroy(stackDrag.gameObject);
    }

    public void MoveSlotItem(Vector2 position, GameObject objectDrag)
    {
        if (!UI_DragDropManager.iDragHandler) return;
        Vector2 Size = new Vector2(45, -45);
        objectDrag.transform.position = position + Size;
    }

    public void SwapSlot(ItemStack fromSlot, ItemStack toSlot)
    {
        ItemStack temp = new ItemStack(fromSlot.GetItem(), fromSlot.GetStack(), fromSlot.GetActive());
        fromSlot.SetItemStack(toSlot);
        toSlot.SetItemStack(temp);

    }



}
