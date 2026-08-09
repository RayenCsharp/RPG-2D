using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [Header("Inventory References")]
    [SerializeField] private GameObject mainInvetory;
    [SerializeField] private GameObject hotBarInventory;
    private List<ItemHolder> MainInventorySlots = new List<ItemHolder>();
    private List<ItemHolder> HotBarSlots = new List<ItemHolder>();
    private List<ItemHolder> HoleInventory = new List<ItemHolder>();
    [Header("Bindings Proprities")]
    private bool inventoryOpen;
    public bool InventoryOpen => inventoryOpen;
    [Header("PickUp Proprities")]
    [SerializeField] private GameObject pickUpItem;
    private ItemPickUp newItem;
    private GameObject hightLightedItem;
    [SerializeField] private Vector2 pickUpRange;
    [SerializeField] private Vector2 offSetRange;
    [SerializeField] private LayerMask layer;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        MainInventorySlots.AddRange(mainInvetory.GetComponentsInChildren<ItemHolder>());
        HotBarSlots.AddRange(hotBarInventory.GetComponentsInChildren<ItemHolder>());
        HoleInventory.AddRange(HotBarSlots);
        HoleInventory.AddRange(MainInventorySlots);
    }

    private void Update()
    {
        PickUpDetection();
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

    void OnInventoryToggle(InputValue value)
    {
        mainInvetory.SetActive(!mainInvetory.activeInHierarchy);
        inventoryOpen = mainInvetory.activeInHierarchy;
    }

    void OnPick (InputValue value)
    {
        if (newItem != null)
        {
            AddItem(newItem.ItemData, 1);
            Destroy(pickUpItem);
            pickUpItem = null;
            newItem = null;
        }
        else
        {
            Debug.Log("Nothing To Pick Up");
        }
    }

    private void PickUpDetection()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            (Vector2)transform.position + offSetRange,
            pickUpRange,
            0f,
            layer
        );
        Collider2D firstPickUp = hits.Length > 0 ? hits[0] : null;

        if (firstPickUp != null)
        {
            Debug.Log(firstPickUp);
            pickUpItem = firstPickUp.gameObject;
            newItem = pickUpItem.GetComponent<ItemPickUp>();
            pickUpItem.GetComponent<SpriteRenderer>().sprite = newItem.HightLightedSprite;
            if (hightLightedItem != null && hightLightedItem != pickUpItem)
            {
                hightLightedItem.GetComponent<SpriteRenderer>().sprite = hightLightedItem.GetComponent<ItemPickUp>().OriginalSprite;
            }
            hightLightedItem = pickUpItem;
        }
        else
        {
            if (pickUpItem != null)
            {
                pickUpItem.GetComponent<SpriteRenderer>().sprite = newItem.OriginalSprite;
                pickUpItem = null;
                newItem = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (newItem != null)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube((Vector2)transform.position + offSetRange, pickUpRange);
    }
}
