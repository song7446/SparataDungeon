using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ������ ���� 
    [Header("Move")]
    // ������ �Է�
    Vector3 curInput;
    // �÷��̾� ������ٵ�
    Rigidbody _rigidbody;
    // �⺻ ���ǵ�
    [SerializeField] private float _speed = 3f;
    // ���������� ���� �� �ִ� �߰� ���ǵ�
    [SerializeField] float plusSpeed = 0f;
    // �⺻ �����Ŀ�
    [SerializeField] private float jumpPower = 3f;
    // ���������� ���� �� �ִ� �߰� ���� �Ŀ� 
    [SerializeField] private float plusJumpPower = 0f;
    // �ٴ� ���̾� ����ũ - ������ �� �ٴڿ� �ִ��� Ȯ�ο�
    [SerializeField] private LayerMask roadLayerMask;
    // �޸��� �ִ��� Ȯ�ο� 
    public bool isRun = false;

    // �þ� ����
    [Header("Look")]
    // ī�޶� �����̳�
    [SerializeField] public Transform cameraContainer;
    // ī�޶� �����ӿ� ���콺 �Է�
    Vector2 mouseDelta;
    // ī�޶� ȸ����
    float cameraRot;
    // ī�޶� ȸ�� �ӵ�
    [SerializeField] private float lookSensitivity;
    // �ּ� ī�޶� ����
    [SerializeField] private float minCameraX;
    // �ִ� ī�޶� ���� 
    [SerializeField] private float maxCameraX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // ���콺 Ŀ�� ��ױ� 
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        // ������ �Լ� 
        Move();
        // �÷��̾ �޸��� ���� �� 
        if (isRun)
        {
            // ���� �޸��� ���� �Լ� 
            StopRun();
        }
    }
    private void LateUpdate()
    {
        // ī�޶� ȸ�� �Լ� 
        CameraLook();
    }

    // ������ �Է� WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        // WASD�� ������ ���� �� �� �޾ƿ���
        if (context.phase == InputActionPhase.Performed)
        {
            curInput = context.ReadValue<Vector2>();
        }
        // WASD�� �ȴ��� ���� ������Ű�� ���� zero�� ����� 
        else if (context.phase == InputActionPhase.Canceled)
        {
            curInput = Vector3.zero;
        }
    }

    // ������ �Լ� 
    void Move()
    {
        // �÷��̾��� �տ� �Էµ� y���� �����ʿ� x�� ���� ���� ��� - z�� �ƴ� y�� ������ �Է��� vector2�� �ޱ� ����
        Vector3 direction = transform.forward * curInput.y + transform.right * curInput.x;
        // �⺻ ���ǵ� + ������ �߰� ���ǵ� 
        direction *= _speed + plusSpeed;
        // y ���� �ӵ� �״�� - �����ϰ� �ִٸ� ���� �ӵ� �״��  
        direction.y = _rigidbody.velocity.y;
        // �̵� ���� 
        _rigidbody.velocity = direction;
    }

    // ���� �Է� �����̽���
    public void OnJump(InputAction.CallbackContext context)
    {
        // �÷��̾ ���� ���� �� ���̽��ٸ� �Է��Ѵٸ� 
        if (context.phase == InputActionPhase.Started && OnGround())
        {
            // �÷��̾��� ������ٵ� �⺻ �����Ŀ��� �߰� ���� �Ŀ��� ���ؼ� ���� ��Ű�� 
            _rigidbody.AddForce(Vector2.up * (jumpPower + plusJumpPower), ForceMode.Impulse);
        }
    }

    // �÷��̾ ���� �ִ��� Ȯ�� �Լ� 
    bool OnGround()
    {
        // �÷��̾��� �ٴڿ� ������ ���
        Ray ray = new Ray(transform.position + (-transform.up * 0.45f), Vector3.down);
        // true��� �ٴڿ� �ְ� false��� �ٴڿ� ���� �� 
        return Physics.Raycast(ray, 0.1f, roadLayerMask);
    }

    // �޸��� �Է� ����Ʈ 
    public void OnDash(InputAction.CallbackContext context)
    {
        // ����Ʈ�� ������ �߿��� ���ǵ� 2�� 
        if (context.phase == InputActionPhase.Performed)
        {
            _speed *= 2f;
            isRun = true;
        }
        // �ƴϸ� �⺻ ���ǵ� ����
        else if (context.phase == InputActionPhase.Canceled)
        {
            _speed = 3f;
            isRun = false;
        }
    }

    // ���� �޸��� ���� �Լ� 
    void StopRun()
    {
        // �÷��̾ �������� �پ��� ���� �޸��� ���� 
        if (CharacterManager.Instance.Player.playerState.GetEnergy() <= 0f)
        {
            // ���� �޸��� ���� 
            _speed = 3f;
        }
    }  

    // ī�޶� ȸ���� ���� ���콺 �Է� �ޱ� 
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // ī�޶� ���� ��� �Լ� 
    void CameraLook()
    {
        // ī�޶� ���� ��� - �Էµ� ���콺 ���� y(vector2�� �Է� �޾Ƽ� y�� ��������� ��� z)�� ���� ���ؼ� �߰����ֱ�
        cameraRot += mouseDelta.y * lookSensitivity;
        // ī�޶� ���� �ִ� �ּ� ���� ����� �ʰ� �ϱ� 
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
