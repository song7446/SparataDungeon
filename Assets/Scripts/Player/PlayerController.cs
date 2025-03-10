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
    // 카메라 컨테이너
    [SerializeField] public Transform cameraContainer;
    // 카메라 움직임용 마우스 입력
    Vector2 mouseDelta;
    // 카메라 회전값
    float cameraRot;
    // 카메라 회전 속도
    [SerializeField] private float lookSensitivity;
    // 최소 카메라 각도
    [SerializeField] private float minCameraX;
    // 최대 카메라 각도 
    [SerializeField] private float maxCameraX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 마우스 커서 잠그기 
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        // 움직임 함수 
        Move();
        // 플레이어가 달리고 있을 때 
        if (isRun)
        {
            // 강제 달리기 금지 함수 
            StopRun();
        }
    }
    private void LateUpdate()
    {
        // 카메라 회전 함수 
        CameraLook();
    }

    // 움직임 입력 WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        // WASD를 누르고 있을 때 값 받아오기
        if (context.phase == InputActionPhase.Performed)
        {
            curInput = context.ReadValue<Vector2>();
        }
        // WASD를 안누를 때는 정지시키기 위해 zero로 만들기 
        else if (context.phase == InputActionPhase.Canceled)
        {
            curInput = Vector3.zero;
        }
    }

    // 움직임 함수 
    void Move()
    {
        // 플레이어의 앞에 입력된 y값과 오른쪽에 x를 곱해 방향 계산 - z가 아닌 y인 이유는 입력을 vector2로 받기 때문
        Vector3 direction = transform.forward * curInput.y + transform.right * curInput.x;
        // 기본 스피드 + 아이템 추가 스피드 
        direction *= _speed + plusSpeed;
        // y 기존 속도 그대로 - 점프하고 있다면 점프 속도 그대로  
        direction.y = _rigidbody.velocity.y;
        // 이동 적용 
        _rigidbody.velocity = direction;
    }

    // 점프 입력 스페이스바
    public void OnJump(InputAction.CallbackContext context)
    {
        // 플레이어가 땅에 있을 때 페이스바를 입력한다면 
        if (context.phase == InputActionPhase.Started && OnGround())
        {
            // 플레이어의 리지드바디에 기본 점프파워와 추가 점프 파워를 더해서 점프 시키기 
            _rigidbody.AddForce(Vector2.up * (jumpPower + plusJumpPower), ForceMode.Impulse);
        }
    }

    // 플레이어가 땅에 있는지 확인 함수 
    bool OnGround()
    {
        // 플레이어의 바닥에 레이저 쏘기
        Ray ray = new Ray(transform.position + (-transform.up * 0.45f), Vector3.down);
        // true라면 바닥에 있고 false라면 바닥에 없는 것 
        return Physics.Raycast(ray, 0.1f, roadLayerMask);
    }

    // 달리기 입력 쉬프트 
    public void OnDash(InputAction.CallbackContext context)
    {
        // 쉬프트를 누르는 중에는 스피드 2배 
        if (context.phase == InputActionPhase.Performed)
        {
            _speed *= 2f;
            isRun = true;
        }
        // 아니면 기본 스피드 복귀
        else if (context.phase == InputActionPhase.Canceled)
        {
            _speed = 3f;
            isRun = false;
        }
    }

    // 강제 달리기 금지 함수 
    void StopRun()
    {
        // 플레이어가 에너지를 다쓰면 강제 달리기 금지 
        if (CharacterManager.Instance.Player.playerState.GetEnergy() <= 0f)
        {
            // 강제 달리기 금지 
            _speed = 3f;
        }
    }  

    // 카메라 회전을 위한 마우스 입력 받기 
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 카메라 각도 계산 함수 
    void CameraLook()
    {
        // 카메라 각도 계산 - 입력된 마우스 값의 y(vector2로 입력 받아서 y로 사용하지만 사실 z)에 감도 곱해서 추가해주기
        cameraRot += mouseDelta.y * lookSensitivity;
        // 카메라 각도 최대 최소 사이 벗어나지 않게 하기 
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
