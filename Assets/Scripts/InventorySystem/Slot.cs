using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventorySystem inventorySys;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventorySys = GameObject.Find("MainCharacter").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemHolder droppedToItem = GetComponentInChildren<ItemHolder>();
        ItemHolder draggedItem = eventData.pointerDrag.GetComponent<ItemHolder>();
        if (draggedItem.EquipmentSlot || droppedToItem.EquipmentSlot)
            return;
        inventorySys.HandleDrop(draggedItem, droppedToItem);

    }
}
