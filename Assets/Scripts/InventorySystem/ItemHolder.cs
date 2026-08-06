using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Ui References")]
    [SerializeField] private GameObject icon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemQuantitytxt;
    [SerializeField] private bool isHovering;
    public bool IsHovering => isHovering;

    [Header("Item Properties")]
    private ItemData itemHeld;
    private int quantityHeld;

    void Awake()
    {
        itemIcon = icon.GetComponent<Image>();
        itemQuantitytxt = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        UpdateSlot();
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

    }
}
