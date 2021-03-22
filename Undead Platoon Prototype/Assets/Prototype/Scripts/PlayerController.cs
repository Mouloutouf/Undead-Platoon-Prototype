using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Title("Move Parameters")]
    public float moveSpeed;

    [Title("Look Parameters")]
    public float lookSpeed; // Mouse Sensitivity;
    public Camera playerCamera;

    private Vector2 direction;
    private Vector2 mousePosition;

    private float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();

        Look();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
    }
    public void LookInput(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Debug.Log(mousePosition);
    }

    public void Move()
    {
        float posX = direction.x;
        float posZ = direction.y;

        Vector3 movement = new Vector3(posX, 0f, posZ);
        Vector3 normalizedMovement = movement.normalized;

        transform.Translate(normalizedMovement * moveSpeed * Time.deltaTime);
    }
    public void Look()
    {
        float mousePosX = mousePosition.x * lookSpeed * Time.deltaTime; // Right/Left
        float mousePosY = mousePosition.y * lookSpeed * Time.deltaTime; // Up/Down

        xRotation -= mousePosY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mousePosX);
    }
}
