using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Ui References")]
    [SerializeField] private Transform dragLayer;
    [SerializeField] private CanvasGroup itemHolderCanvas;
    [SerializeField] private GameObject icon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemQuantitytxt;
    private Transform OrignalParent;

    [Header("Item Properties")]
    private ItemData itemHeld;
    private int quantityHeld;

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
        transform.SetParent(dragLayer);
        itemHolderCanvas.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(OrignalParent);
        itemHolderCanvas.blocksRaycasts = true;
    }
}
