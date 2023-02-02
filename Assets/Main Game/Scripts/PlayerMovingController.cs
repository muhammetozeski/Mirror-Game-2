using UnityEngine.InputSystem;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovingController : MonoBehaviour
{
    [Header("Character Moving Values")]
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float Gravity = -10f;
    [SerializeField] private float JumpHeight = 5f;

    [Header("Character Rotating Values")]
    [SerializeField] private float MouseSpeed = 10f;

    [Header ("Component assingments")]
    [SerializeField] Transform FPSCharacterCamera;

    InputManager inputManager;
    InputAction Move, Look, Jump;

    CharacterController characterController;
    
    private Vector3 velocity;
    float lookToY = 0;
    private void Start()
    {
        //TODO: make a CursorLockStateModeManager
        Cursor.lockState = CursorLockMode.Locked;

        inputManager = GameManager.Instance.inputManager;

        #region Input Action Assigns
        Move = inputManager.Move;
        Look = inputManager.Look;
        Jump = inputManager.Jump;
        #endregion

        Jump.started += CharacterJump;

        characterController = GetComponent<CharacterController>();
    }
    /// <summary>
    /// debug purpose. it lets to rotate player
    /// </summary>
    [SerializeField] bool letToRotate;
    private void Update()
    {
        //debug purpose:
        if (Input.GetKeyDown(KeyCode.R))
        {
            letToRotate = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            letToRotate = true;
        }

        #region Character Moving

        //don't fly for some reason but don't pass down the ground also:
        if (characterController.isGrounded && velocity.y < 0) velocity.y = -2f;

        Vector2 moveTo = Move.ReadValue<Vector2>();
        Vector3 move = transform.right * moveTo.x + transform.forward * moveTo.y;

        characterController.Move(move * Speed * Time.deltaTime);

        velocity.y += Gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
        #endregion
        if (letToRotate)
        {
        #region Character Rotating
        Vector2 lookTo = Look.ReadValue<Vector2>();
        //do not use Time.deltaTime for rotating. it causes some problems. sometimes when screen is rotating, rotation jumps.
        float mouseX = lookTo.x * MouseSpeed; //* Time.deltaTime;
        lookToY -= lookTo.y * MouseSpeed; //* Time.deltaTime;
            lookToY = Mathf.Clamp(lookToY, -89, 89);
        transform.Rotate(Vector3.up * mouseX);

            Vector3 l = new Vector3();
            l.x = lookToY;
            FPSCharacterCamera.localRotation = Quaternion.Euler(
               lookToY, 0, 0);
        #endregion
        }
    }

    void CharacterJump(InputAction.CallbackContext ctx)
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
