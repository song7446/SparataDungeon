using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    Vector3 curInput;
    Rigidbody _rigidbody;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float jumpPower = 3f;
    [SerializeField] private LayerMask roadLayerMask;
    public bool isRun = false;

    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    Vector2 mouseDelta;
    float cameraRot;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float minCameraX;
    [SerializeField] private float maxCameraX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curInput = Vector3.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && OnGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _speed *= 2f;
            isRun = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _speed = 3f;
            isRun = false;
        }
    }
    void Move()
    {
        Vector3 direction = transform.forward * curInput.y + transform.right * curInput.x;
        direction *= _speed;
        direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }


    bool OnGround()
    {
        Ray ray = new Ray(transform.position + (-transform.up * 0.45f), Vector3.down);
        return Physics.Raycast(ray, 0.1f, roadLayerMask);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook()
    {
        cameraRot += mouseDelta.y * lookSensitivity;
        cameraRot = Mathf.Clamp(cameraRot, minCameraX, maxCameraX);
        cameraContainer.localEulerAngles = new Vector3(-cameraRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
