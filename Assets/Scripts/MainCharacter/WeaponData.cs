using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] private float attackTime;
    [SerializeField] private Vector2 weaponRange;
    [SerializeField] private Vector2 offSetRange;
    [SerializeField] private float startDamage;
    [SerializeField] private float endDamage;
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private AnimationClip attackAnimation;

    public string Name => weaponName;
    public int Damage => damage;
    public float Knockback => knockback;
    public float AttackTime => attackTime;
    public Vector2 WeaponRange => weaponRange;
    public Vector2 OffsetRange => offSetRange;

    public float StartDamage => startDamage;
    public float EndDamage => endDamage;
    public Sprite Icon => weaponIcon;
    public AnimationClip AttackAnimation => attackAnimation;
}

