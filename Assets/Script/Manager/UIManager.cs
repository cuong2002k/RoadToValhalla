using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    [SerializeField] private static Transform canvas;
    [SerializeField] private GameObject _playerInventoryPanel;
    [SerializeField] private GameObject _craftingPanel;

    [SerializeField] private GameObject _mousePoint;

    #region Inventory Handler
    [HideInInspector] public SlotUI slotMove;
    [HideInInspector] public Image iconDrag;
    [HideInInspector] public TextMeshProUGUI stackDrag;
    [HideInInspector] public static bool iDragHandler = false;

    #endregion

    private void Start()
    {
        DontDestroyOnLoad(this);
        canvas = this.transform;
    }

    private void OnEnable()
    {
        InputManager.OpenInventoryEvent += HandlerUIInput;
    }

    private void OnDisable()
    {
        InputManager.OpenInventoryEvent -= HandlerUIInput;
    }

    private void Update()
    {
        if (iDragHandler)
        {
            MoveSlotItem(Input.mousePosition, stackDrag.gameObject);
            MoveSlotItem(Input.mousePosition, iconDrag.gameObject);
        }

        _mousePoint.gameObject.transform.position = Input.mousePosition;

    }

    public void HandlerUIInput()
    {
        if (_playerInventoryPanel.activeSelf)
        {
            _playerInventoryPanel.SetActive(false);
            _craftingPanel.SetActive(false);
            RestartMouseSlot();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _playerInventoryPanel.SetActive(true);
            _craftingPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
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
                    Debug.Log("Combine");
                }
                else if (fromSlot.GetStack() == this.slotMove.GetItemUI().GetStack())
                {
                    SwapSlot(fromSlot, toSlot);
                    Debug.Log("swap Not empty equal item");
                }
            }
            else
            {
                if (fromSlot.GetStack() == this.slotMove.GetItemUI().GetStack())
                {
                    SwapSlot(fromSlot, toSlot);
                    Debug.Log("swap Not empty not equal full stack");
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
                Debug.Log("split to empty slot");
            }
            else
            {
                SwapSlot(fromSlot, toSlot);
                Debug.Log("swap to empty slot");
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
        if (!UIManager.iDragHandler) return;
        Vector2 Size = new Vector2(45, -45);
        objectDrag.transform.position = position + Size;
    }

    public void SwapSlot(ItemStack fromSlot, ItemStack toSlot)
    {
        ItemStack temp = new ItemStack(fromSlot.GetItem(), fromSlot.GetStack());
        fromSlot.SetItemStack(toSlot);
        toSlot.SetItemStack(temp);

    }

    public void DropItemSlotUI()
    {
        if (this.slotMove != null)
        {
            int slotid = slotMove.GetSlotID();
            ItemStack itemDrop = slotMove.Inventory[slotid];
            itemDrop.DropItem(slotMove.GetItemUI());
            this.slotMove.Inventory.Invoke();
            this.RestartMouseSlot();
        }
    }

}
