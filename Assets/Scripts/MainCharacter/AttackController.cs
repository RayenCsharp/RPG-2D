using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponBehavour weaponBehavour;
    [SerializeField] private InventorySystem inventorySystem;

    [SerializeField] private Transform weaponEquipped;
    [SerializeField] private CapsuleCollider2D weaponCollider;

    [SerializeField] private ToolData equippedTool;
    [SerializeField] private float offSetDistance = 0.5f;


    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        inventorySystem = GetComponent<InventorySystem>();
        weaponBehavour = GetComponentInChildren<WeaponBehavour>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (equippedTool != null)
        {
            weaponEquipped.position = (Vector2)transform.position + playerController.LastMovementDirection * offSetDistance;
            weaponEquipped.rotation = Quaternion.LookRotation(Vector3.forward, -playerController.LastMovementDirection);
            weaponCollider.size = new Vector2(equippedTool.ToolRange.x, weaponCollider.size.y);
            weaponCollider.offset = equippedTool.OffsetRange;

            weaponBehavour.EquipWeapon(equippedTool);
        }
    }

    public void EquipTool(ToolData tool)
    {
        equippedTool = tool;
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && playerController.canAttack && !inventorySystem.InventoryOpen && equippedTool != null)
        {
            float attackSpeedMultiplier = 0.45f / equippedTool.AnimationDuration;
            playerAnimator.SetFloat("AttackSpeedMultiplier", attackSpeedMultiplier);
            playerAnimator.SetTrigger("Attack");
            weaponBehavour.PlayAttackAnimation();
        }
    }

    
}
