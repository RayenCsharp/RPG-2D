using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Ui References")]
    [SerializeField] private Transform dragLayer;
    [SerializeField] private CanvasGroup itemHolderCanvas;
    [SerializeField] private GameObject icon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemQuantitytxt;
    private Transform OrignalParent;
    [SerializeField] private bool isHovering;
    public bool IsHovering => isHovering;

    [Header("Item Properties")]
    private ItemData itemHeld;
    private int quantityHeld;
    [Header("Equipment Slot Proprities")]
    [SerializeField] private bool equipmentSlot;
    public bool EquipmentSlot => equipmentSlot; // will be used for Slot script to block Drop Event
    public enum Equipment_Type { None, Tool}
    [SerializeField] private Equipment_Type equipmentType;
    public Equipment_Type EquipmentType => equipmentType;

    void Awake()
    {
        itemIcon = icon.GetComponent<Image>();
        itemQuantitytxt = GetComponentInChildren<TMP_Text>();
        itemHolderCanvas = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        UpdateSlot();
        OrignalParent = transform.parent;

    }
    
    public ItemData ItemHeld => itemHeld;
    public int QuantityHeld => quantityHeld;

    public void UpdateSlot()
    {
        if (itemHeld != null)
        {
            itemIcon.enabled = true;
            itemIcon.sprite = itemHeld.Icon;
            itemQuantitytxt.text = quantityHeld.ToString();
        }
        else
        {
            itemIcon.enabled = false;
            itemQuantitytxt.text = "";
        }
    }

    public void SetItem(ItemData item, int quantity)
    {
        itemHeld = item;
        quantityHeld = quantity;
        UpdateSlot();
    }

    public int AddAmmount(int amount)
    {
        quantityHeld += amount;
        UpdateSlot();
        return quantityHeld;
    }

    public int RemoveAmmount(int amount)
    {
        quantityHeld -= amount;
        if (quantityHeld == 0)
        {
            clearSlot();
        }
        else
        {
            UpdateSlot();
        }
        return quantityHeld;
    }

    private void clearSlot()
    {
        itemHeld = null;
        quantityHeld = 0;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return itemHeld != null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!equipmentSlot)
        {
            transform.SetParent(dragLayer);
            itemHolderCanvas.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!equipmentSlot)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!equipmentSlot)
        {
            transform.SetParent(OrignalParent);
            itemHolderCanvas.blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
