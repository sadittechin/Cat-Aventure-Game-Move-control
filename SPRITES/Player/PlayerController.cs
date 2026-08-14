using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 1f;

    private Rigidbody2D rb;

    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // เดิน A / D
        moveAction = new InputAction("Move", InputActionType.Value);

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");

        // กระโดด Space
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        // หันซ้าย-ขวา
        float move = moveAction.ReadValue<float>();

        if (move > 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        // กระโดด
        if (jumpAction.WasPressedThisFrame())
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }
    }

    void FixedUpdate()
    {
        float move = moveAction.ReadValue<float>();

        rb.linearVelocity = new Vector2(
            move * moveSpeed,
            rb.linearVelocity.y
        );
    }
}