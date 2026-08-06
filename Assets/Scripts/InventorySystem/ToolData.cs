using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class ToolData : ItemData
{
    public enum ToolType { Sword, Bow, Axe, Pickaxe, Shovel, Hoe }
    [Header("Tool Properties")]
    [SerializeField] private ToolType toolType;
    [SerializeField] private float durability;
    [SerializeField] private float damage;
    [SerializeField] private float knockback;
    [SerializeField] private float speed;
    [Header("Collider Properties")]
    [SerializeField] private Vector2 toolRange;
    [SerializeField] private Vector2 offSetRange;
    [Header("Animation Properties")]
    [SerializeField] private float startDamage;
    [SerializeField] private float endDamage;
    [SerializeField] private float animationDuration;
    [SerializeField] private AnimationClip attackAnimation;


    public ToolType tool_type => toolType;
    public float Durability => durability;
    public float Damage => damage;
    public float Knockback => knockback;
    public float Speed => speed;
    public Vector2 ToolRange => toolRange;
    public Vector2 OffsetRange => offSetRange;
    public float AnimationDuration => animationDuration;
    public float StartDamage => startDamage;
    public float EndDamage => endDamage;
    public AnimationClip AttackAnimation => attackAnimation;
}
