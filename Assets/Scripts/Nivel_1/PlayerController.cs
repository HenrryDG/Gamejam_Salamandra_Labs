using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    // PONER HEADERS
    [Header("Movimiento")]
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float runSpeed = 7f;

    [Header("Velocidad de Animación")]
    [SerializeField] private float walkAnimationSpeed = 0.8f;
    [SerializeField] private float runAnimationSpeed = 1.1f;

    private bool isGrounded = true;

    Rigidbody rb;
    PlayerInput actionsMaps;

    private bool isRunning = false;
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
            if (rb != null && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (animator != null)
                {
                    animator.SetTrigger("Jump");
                }
            }
            else
            {
                Debug.LogWarning("PlayerController.Jump: No se puede saltar — no está en el suelo o falta Rigidbody.", this);
            }
        }
    }


    // Método para correr
    public void Run(InputAction.CallbackContext context)
    {
        // Verifica si la acción acaba de empezar 
        if (context.started)
        {
            isRunning = true;
        }
        // Verifica si la acción acaba de terminar
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    // Método para moverse

    void MovePlayerInput()
    {
        if (actionsMaps == null)
        {
            return;
        }

        var moveAction = actionsMaps.actions?.FindAction("Move");
        if (moveAction == null)
        {
            return;
        }

        Vector2 inputs = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputs.x, 0, inputs.y);

        float currentSpeed = moveSpeed;

        // El sprint solo aplica si se hay movimiento y se tiene Shift presionado
        if (inputs.y > 0.01f && isRunning)
        {
            currentSpeed = runSpeed;
            if (animator != null)
            {
                // Asigna la velocidad de animación de correr
                animator.speed = runAnimationSpeed;
            }
        }
        else
        {
            // --- INICIO DE CAMBIOS ---
            // Si no está corriendo (o no se está moviendo hacia adelante), vuelve a la velocidad normal
            if (animator != null)
            {
                animator.speed = walkAnimationSpeed;
            }
            // --- FIN DE CAMBIOS ---
        }

        if (rb != null)
        {
            Vector3 targetPos = rb.position + transform.TransformDirection(move) * currentSpeed * Time.deltaTime;
            rb.MovePosition(targetPos);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", inputs.magnitude);
        }
    }

    // Método para rotar la cámara

    void MoveCamera()
    {
        Vector2 inputs = actionsMaps.actions["MoveCamera"].ReadValue<Vector2>();
        hMouse = mouse_horizontal * inputs.x;

        vMouse += mouse_vertical * inputs.y;
        vMouse = Mathf.Clamp(vMouse, maxRotationLookUp, maxRotationLookDown);

        Camera.main.transform.localEulerAngles = new Vector3(-vMouse, 0, 0.0f);
        transform.Rotate(0, hMouse, 0);
    }

    // Metodo para verificar si puede saltar
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
