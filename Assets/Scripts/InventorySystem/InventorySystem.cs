using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemData Stone;
    [SerializeField] private ItemData Sword;
    [Header("Inventory References")]
    [SerializeField] private GameObject mainInvetory;
    [SerializeField] private GameObject hotBarInventory;
    private List<ItemHolder> MainInventorySlots = new List<ItemHolder>();
    private List<ItemHolder> HotBarSlots = new List<ItemHolder>();
    private List<ItemHolder> HoleInventory = new List<ItemHolder>();
    //[Header("Dragging Properties")]
    //[SerializeField] private Image draggedItemUi;
    //[SerializeField] private ItemHolder draggedItem;
    //private bool isDragging = false;

    void Awake()
    {
        MainInventorySlots.AddRange(mainInvetory.GetComponentsInChildren<ItemHolder>());
        HotBarSlots.AddRange(hotBarInventory.GetComponentsInChildren<ItemHolder>());
        HoleInventory.AddRange(MainInventorySlots);
        HoleInventory.AddRange(HotBarSlots);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(Stone, 5);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddItem(Sword, 1);

        }
        //OnDrageStart();
        //OnDrageEnd();
        //DragUi();
    }

    public void AddItem(ItemData item, int amount)
    {
        int remaining = amount;
        foreach (ItemHolder slot in HoleInventory)
        {
            if (slot.ItemHeld == item)
            {
                int availbeSpace = item.MaxAmount - slot.QuantityHeld;
                if (availbeSpace > 0)
                {
                    int amountToAdd = Mathf.Min(availbeSpace, remaining);
                    slot.AddAmmount(amountToAdd);
                    remaining -= amountToAdd;
                    if (remaining <= 0)                    
                        return;
                }
            }
        }
        foreach (ItemHolder slot in HoleInventory)
        {
            if (!slot.HasItem())
            {
                int amountToAdd = Mathf.Min(item.MaxAmount, remaining);
                slot.SetItem(item, amountToAdd);
                remaining -= amountToAdd;
                if (remaining <= 0)
                    return;
            }
        }
        if (remaining > 0)
        {
            Debug.Log("Not enough space in inventory to add " + item.Name + ". Ammount: " + remaining);
        }
    }

    //private ItemHolder GetHoveredItem()
    //{
    //    foreach (ItemHolder slot in HoleInventory)
    //    {
    //        if (slot.IsHovering)
    //        {
    //            return slot;
    //        }
    //    }
    //    return null;
    //}

    //private void OnDrageStart()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        ItemHolder HoveredItem = GetHoveredItem();
    //        if (HoveredItem != null && HoveredItem.HasItem())
    //        {
    //            draggedItem = HoveredItem;
    //            draggedItemUi = HoveredItem.GetComponent<Image>();
    //            isDragging = true;
    //        }
    //    }
    //}

    //private void OnDrageEnd()
    //{
    //    if (Input.GetMouseButtonUp(0) && isDragging)
    //    {
    //        ItemHolder HoveredItem = GetHoveredItem();
    //        if (HoveredItem != null)
    //        {
    //            HandleDrop(draggedItem, HoveredItem);
    //            isDragging = false;
    //            draggedItem = null;
    //        }
    //    }
    //}

    public void HandleDrop(ItemHolder draggedItem, ItemHolder draggedTo)
    {
        if (draggedItem == draggedTo || draggedItem == null || draggedTo == null)
        {
            Debug.Log("dragged item :" + draggedItem + "dragged to:" + draggedTo);
            return;
        }
        //stacking
        if (draggedItem.ItemHeld == draggedTo.ItemHeld)
        {
            int draggedAmount = draggedItem.QuantityHeld;
            int availableSpace = draggedTo.ItemHeld.MaxAmount - draggedTo.QuantityHeld;
            if (availableSpace > 0)
            {
                int amountToAdd = Mathf.Min(draggedAmount, availableSpace);
                draggedItem.RemoveAmmount(amountToAdd);
                draggedTo.AddAmmount(amountToAdd);
            }
        }
        //Changing Slot
        else
        {
            ItemData tmpItem = draggedTo.ItemHeld;
            int tmpAmmount = draggedTo.QuantityHeld;
            draggedTo.SetItem(draggedItem.ItemHeld, draggedItem.QuantityHeld);
            draggedItem.SetItem(tmpItem, tmpAmmount);
        }
    }

    //private void DragUi()
    //{
    //    if (draggedItemUi != null)
    //    {
    //        if (isDragging)
    //        {
    //            draggedItemUi.transform.position = Input.mousePosition;
    //            //draggedItemUi.transform.parent.SetAsFirstSibling();
    //            draggedItemUi.raycastTarget = false;
    //        }
    //        else
    //        {
    //            draggedItemUi.transform.localPosition = Vector3.zero;
    //            draggedItemUi.raycastTarget = true;
    //            draggedItemUi = null;
    //        }
    //    }
    //}
}
