using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite hightLightedSprite;
    [SerializeField] private ItemData itemData;
    private Rigidbody2D rb;
    [SerializeField] private float dragResistence;

    public Sprite OriginalSprite => originalSprite;
    public Sprite HightLightedSprite => hightLightedSprite;
    public ItemData ItemData => itemData;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = dragResistence;
    }
}
