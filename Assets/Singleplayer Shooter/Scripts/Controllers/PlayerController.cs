using AimLab;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpForce = 1.5f;
    public float sprintSpeed = 8f;


    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraHolder;

    [Space]
    [SerializeField] private Recoil _recoil;
    [SerializeField] private GameObject _crossHair;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    private bool _isMovementEnabled = false;

    public bool IsMovementEnabled => _isMovementEnabled;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ToggleMovement(false);
        AimLabManager.OnGameModeSelected += HandleGameModeUpdate;
    }

    private void OnDestroy()
    {
        AimLabManager.OnGameModeSelected += HandleGameModeUpdate;
    }

    private void HandleGameModeUpdate(bool started, AimDifficulty difficulty)
    {
        if (started)
        {
            Debug.Log("[NRM] Starting the game enabling the movement");
            ToggleCrossHair(true);
            ToggleMovement(true);
        }
        else
        {
            ToggleCrossHair(false);
            ToggleMovement(false);
        }
    }

    void Update()
    {
        if (_isMovementEnabled)
        {
            HandleMouseLook();
            //HandleMovement();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Quaternion baseRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraHolder.localRotation = baseRotation * _recoil.GetRecoilRotation();

        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        controller.Move(move * speed * Time.deltaTime);

        // Ground Check
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Jump
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void ToggleCrossHair(bool show)
    {
        if(_crossHair != null)
            _crossHair.SetActive(show);
    }

    private void ToggleMovement(bool isOn)
    {
        _isMovementEnabled = isOn;

        if (isOn)
            Cursor.lockState = CursorLockMode.Locked;
    }
}