using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
     #region VARIABLES

    // PONER HEADERS
    [Header("Movimiento")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 5f;

    Rigidbody rb;
    PlayerInput actionsMaps;

    float hMouse, vMouse;

    //Animator controller
    Animator animator;


    [Header("Sensibilidad")]
    [SerializeField] float mouse_horizontal = 0.2f;
    [SerializeField] float mouse_vertical = 0.2f;

    [Header("Rotación")]
    [SerializeField] float maxRotationLookUp = -25.0f;
    [SerializeField] float maxRotationLookDown = 25.0f;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        actionsMaps = GetComponent<PlayerInput>();
        //Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody not found on the GameObject. Movement will not work.", this);
        }

        if (actionsMaps == null)
        {
            Debug.LogWarning("PlayerController: PlayerInput component not found. Input actions will be ignored.", this);
        }

        if (animator == null)
        {
            Debug.LogWarning("PlayerController: Animator component not found. Animation triggers will be skipped.", this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerInput();
        MoveCamera();

    }

    // Método para saltar
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("PlayerController.Jump: Rigidbody is null — cannot apply jump force.", this);
            }

            if (animator != null)
            {
                animator.SetTrigger("jump");
            }
        }
    }

    // Método para moverse

    void MovePlayerInput()
    {
        if (actionsMaps == null)
        {
            // PlayerInput missing; nothing to read
            return;
        }

        var moveAction = actionsMaps.actions?.FindAction("Move");
        if (moveAction == null)
        {
            // Action not found or actions asset null
            // Only log once to avoid spamming the console
            // (developer can enable verbose logging if needed)
            return;
        }

        Vector2 inputs = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputs.x, 0, inputs.y);

        if (rb != null)
        {
            Vector3 targetPos = rb.position + transform.TransformDirection(move) * moveSpeed * Time.deltaTime;
            rb.MovePosition(targetPos);
        }

        if (animator != null)
        {
            animator.SetTrigger("run");
        }
    }

    // Método para rotar la cámara

    void MoveCamera()
    {
        if (actionsMaps == null)
            return;

        var lookAction = actionsMaps.actions?.FindAction("MoveCamera");
        if (lookAction == null)
            return;

        Vector2 inputs = lookAction.ReadValue<Vector2>();
        hMouse = mouse_horizontal * inputs.x;

        vMouse += mouse_vertical * inputs.y;
        vMouse = Mathf.Clamp(vMouse, maxRotationLookUp, maxRotationLookDown);

        if (Camera.main != null)
        {
            Camera.main.transform.localEulerAngles = new Vector3(-vMouse, 0, 0.0f);
        }
        else
        {
            // Camera.main can be null in some contexts (disabled or not tagged)
            // Avoid throwing an exception; developer should ensure there's a MainCamera with the MainCamera tag
        }

        transform.Rotate(0, hMouse, 0);
    }
}
