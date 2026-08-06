using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string id;
    [Header("Ui Properties")]
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [Header("Item Properties")]
    [SerializeField] private int maxAmount;
    public enum dataType { Consumable, Tool, Other, Placeable }
    [SerializeField] private dataType type;
    [Header("Placeable Properties")]
    [SerializeField] private GameObject pickUpPrefab;

    public string Id => id;
    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public int MaxAmount => maxAmount;
    public dataType Type => type;
    public GameObject PickUpPrefab => pickUpPrefab;

}
