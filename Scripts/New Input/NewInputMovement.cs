using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class NewInputMovement : MonoBehaviour
{
    //Movimiento jugador
    private PlayerInput playerInput;
    private CharacterController characterController;
    private float speed = 6f;
    private float gravity = -9.81f;

    private Vector2 playerMovement;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    private Vector3 velocity;

    //Movimiento Cámara
    Vector2 cursor;
    private Vector2 camMovement;
    private float mouseBaseSensitivity = 5f;
    private float mouseCurrentSensitivity;
    private float xRotation = 0f;

    public Transform playerCam;
    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        mouseCurrentSensitivity = mouseBaseSensitivity;
        cursor = new Vector2(Screen.width, Screen.height)/2;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento jugador
        playerMovement = playerInput.actions["PlayerMove"].ReadValue<Vector2>();
        camMovement = playerInput.actions["CamMove"].ReadValue<Vector2>();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if(playerInput.currentActionMap == playerInput.actions.FindActionMap("Explore")){
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            Vector3 move = transform.right * playerMovement.x + transform.forward * playerMovement.y;
            characterController.Move(move * speed * Time.deltaTime);
        }

        //Movimiento cámara
        if(playerInput.currentControlScheme == "Keyboard"){
            mouseCurrentSensitivity = mouseBaseSensitivity;
        }
        else if(playerInput.currentControlScheme == "Gamepad"){
            mouseCurrentSensitivity = mouseBaseSensitivity * 20;
        }

        float mouseX = camMovement.x * mouseCurrentSensitivity * Time.deltaTime;
        float mouseY = camMovement.y * mouseCurrentSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(0, mouseX, 0);

        /* if(playerInput.currentActionMap == playerInput.actions.FindActionMap("Puzzle")){
            cursor += camMovement * mouseBaseSensitivity;
            cursor.x = Mathf.Clamp(cursor.x, 0, Screen.width);
            cursor.y = Mathf.Clamp(cursor.y, 0, Screen.height);
            Mouse.current.WarpCursorPosition(cursor);
            InputState.Change(Mouse.current.position, cursor);
        } */
    }
}
