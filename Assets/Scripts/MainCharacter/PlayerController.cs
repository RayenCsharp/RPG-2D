using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private InventorySystem inventorySystem;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 movementInput;
    private Vector2 movementDirection;
    private Vector2 lastMovementDirection;
    public Vector2 LastMovementDirection { get { return lastMovementDirection; } }
    [SerializeField] private bool isMoving;
    public bool canAttack;
    public bool canMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        inventorySystem = GetComponent<InventorySystem>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        movementDirection = movementInput.normalized;
    }

    private void Move()
    {
        
        if (!canMove || inventorySystem.InventoryOpen)
        {
            rigidbody.linearVelocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
            return;
        }
        rigidbody.linearVelocity = movementDirection * moveSpeed;
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (movementDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (movementDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (movementDirection != Vector2.zero)
        {
            isMoving = true;
            lastMovementDirection = movementDirection;
            
        }
        else
        {
            isMoving = false;
        }
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", lastMovementDirection.x);
        animator.SetFloat("MoveY", lastMovementDirection.y);
    }
}
