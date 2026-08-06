using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponBehavour weaponBehavour;

    [SerializeField] private Transform weaponEquipped;
    [SerializeField] private CapsuleCollider2D weaponCollider;

    [SerializeField] private WeaponData equipedWeapon;
    [SerializeField] private float offSetDistance = 0.5f;


    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        weaponBehavour = GetComponentInChildren<WeaponBehavour>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponEquipped.position = (Vector2)transform.position + playerController.LastMovementDirection * offSetDistance;
        weaponEquipped.rotation = Quaternion.LookRotation(Vector3.forward, -playerController.LastMovementDirection);
        weaponCollider.size = new Vector2(equipedWeapon.WeaponRange.x, weaponCollider.size.y);
        weaponCollider.offset = equipedWeapon.OffsetRange;

        weaponBehavour.EquipWeapon(equipedWeapon);
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && playerController.canAttack)
        {
            float attackSpeedMultiplier = 0.45f / equipedWeapon.AttackTime;
            playerAnimator.SetFloat("AttackSpeedMultiplier", attackSpeedMultiplier);
            playerAnimator.SetTrigger("Attack");
            weaponBehavour.PlayAttackAnimation();
        }
    }

    
}
