using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite hightLightedSprite;
    [SerializeField] private ItemData itemData;

    public Sprite OriginalSprite => originalSprite;
    public Sprite HightLightedSprite => hightLightedSprite;
    public ItemData ItemData => itemData;
}
