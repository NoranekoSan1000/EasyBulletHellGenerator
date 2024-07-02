using UnityEngine;

namespace EasyBulletHellGenerator
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float shiftMultiplier = 1.5f;

        private float rotationX = 0f;
        private float rotationY = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleMovement();
            HandleMouseLook();
        }

        private void HandleMovement()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float moveUp = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                moveUp = 1f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                moveUp = -1f;
            }

            float currentMoveSpeed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentMoveSpeed *= shiftMultiplier;
            }

            Vector3 movement = new Vector3(moveHorizontal, moveUp, moveVertical);
            Vector3 moveDirection = transform.TransformDirection(movement) * currentMoveSpeed * Time.deltaTime;

            transform.position += moveDirection;
        }

        private void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}
