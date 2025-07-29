using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] InputActionAsset inputActions;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float runSpeed = 8f;

    [Header("Look Settings")]
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    InputAction lookAction;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction runAction;

    Vector2 moveInput;
    Vector2 lookInput;
    Vector3 velocity;
    bool isJumping;
    bool isRunning;
    bool isMoving;
    bool wasGrounded;

    float verticalLookRotation;
    private SoundManager soundManager; //حركة الكاميرا


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        soundManager = FindAnyObjectByType<SoundManager>();
        Cursor.lockState = CursorLockMode.Locked;           
        Cursor.visible = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = inputActions.FindAction("Player/Move");
        jumpAction = inputActions.FindAction("Player/Jump");
        lookAction = inputActions.FindAction("Player/Look");
        runAction = inputActions.FindAction("Player/Run");

        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;

        jumpAction.performed += ctx => isJumping = true;
        jumpAction.canceled += ctx => isJumping = false;

        lookAction.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => lookInput = Vector2.zero;

        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
        runAction.Enable();
    }

    void Update()
    {

        HandleLook();
        HandleMovement();

        
    }

    void HandleLook()
    {
        // تدوير اللاعب يمين وشمال
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity);

        // تدوير الكاميرا فوق وتحت
        verticalLookRotation -= lookInput.y * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80f, 80f);
        cameraTransform.localEulerAngles = Vector3.right * verticalLookRotation;
    }

    void HandleMovement()
    {


        // نجيب الاتجاه بحسب الكاميرا
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // نتحقّق إذا كان اللاعب يركض
        bool isRunning = Keyboard.current.leftShiftKey.isPressed;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (moveInput.magnitude > 0.1f && controller.isGrounded)
        {
            if (!isMoving)
            {
                soundManager?.PlaySound("Walk");
                isMoving = true;
            }
        }
        else if (isMoving)
        {
            // لو عندك دالة لإيقاف الصوت، نادها هنا
            isMoving = false;
        }

        //السرعة ف اتجلة ال y

        velocity.y += gravity * Time.deltaTime;

        // نحرّك اللاعب رأسياً
        controller.Move(velocity * Time.deltaTime);

        // قفز
        if (controller.isGrounded && isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            if (soundManager != null)
            {
                soundManager.PlaySound("Jump");
            }
        }

        // نثبت على الأرض لو لمسها
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (!wasGrounded)
            {
                soundManager?.PlaySound("Land");
            }

        }
        wasGrounded = controller.isGrounded;
    }  
    
}
