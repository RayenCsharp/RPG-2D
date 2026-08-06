using UnityEditor.Animations;
using UnityEngine;

public class WeaponBehavour : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Collider2D attackZoneCollider;
    private AnimatorOverrideController overrideController;

    private WeaponData equipedWeapon;

    void Awake()
    {
        weaponAnimator = GetComponent<Animator>();
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        overrideController = new AnimatorOverrideController(weaponAnimator.runtimeAnimatorController);
        weaponAnimator.runtimeAnimatorController = overrideController;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.LastMovementDirection.y > 0.7)
        {
            weaponSpriteRenderer.sortingOrder = 0;

        }
        else
        {
            weaponSpriteRenderer.sortingOrder = 2;
        }
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        equipedWeapon = weaponData;
    }

    public void PlayAttackAnimation()
    {
        overrideController["Empty"] = equipedWeapon.AttackAnimation;
        weaponAnimator.SetTrigger("Attack");
        Invoke("EnableAttackZone", equipedWeapon.StartDamage);
        Invoke("DisableAttackZone", equipedWeapon.EndDamage);
    }

    private void EnableAttackZone ()
    {
        attackZoneCollider.enabled = true;
    }
    private void DisableAttackZone()
    {
        attackZoneCollider.enabled = false;
    }
}
