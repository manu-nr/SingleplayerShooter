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

    [SerializeField] private Recoil _recoil;
    [SerializeField] private GameObject _crossHair;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        WeaponManager.OnGunChange += HandleGunChange;
    }

    private void OnDestroy()
    {
        WeaponManager.OnGunChange -= HandleGunChange;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
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

    private void HandleGunChange(WeaponData data)
    {
        if(!_crossHair.activeSelf)
            _crossHair.SetActive(true);
    }
}