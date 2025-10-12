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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Método para moverse

    void MovePlayerInput()
    {
        Vector2 inputs = actionsMaps.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(inputs.x, 0, inputs.y);

        Vector3 targetPos = rb.position + transform.TransformDirection(move) * moveSpeed * Time.deltaTime;
        rb.MovePosition(targetPos);

        
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
}
