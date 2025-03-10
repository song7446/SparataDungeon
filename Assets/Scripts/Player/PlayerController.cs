using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 움직임 관련 
    [Header("Move")]
    // 움직임 입력
    Vector3 curInput;
    // 플레이어 리지드바디
    Rigidbody _rigidbody;
    // 기본 스피드
    [SerializeField] private float _speed = 3f;
    // 아이템으로 얻을 수 있는 추가 스피드
    [SerializeField] float plusSpeed = 0f;
    // 기본 점프파워
    [SerializeField] private float jumpPower = 3f;
    // 아이템으로 얻을 수 있는 추가 점프 파워 
    [SerializeField] private float plusJumpPower = 0f;
    // 바닥 레이어 마스크 - 점프할 때 바닥에 있는지 확인용
    [SerializeField] private LayerMask roadLayerMask;
    // 달리고 있는지 확인용 
    public bool isRun = false;

    // 시야 관련
    [Header("Look")]
    [SerializeField] public Transform cameraContainer;
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
        if (isRun)
        {
            StopRun();
        }
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
            _rigidbody.AddForce(Vector2.up * (jumpPower + plusJumpPower), ForceMode.Impulse);
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

    void StopRun()
    {
        if (CharacterManager.Instance.Player.playerState.GetEnergy() <= 0f)
        {
            _speed = 3f;
        }
    }

    void Move()
    {
        Vector3 direction = transform.forward * curInput.y + transform.right * curInput.x;
        direction *= _speed + plusSpeed;
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

    public void ChangeSpeed(float amount)
    {
        plusSpeed = amount;
    }

    public void ReturnSpeed()
    {
        plusSpeed = 0f;
    }

    public void ChangeJump(float amount)
    {
        plusJumpPower = amount;
    }

    public void ReturnJump()
    {
        plusJumpPower = 0f;
    }
}
