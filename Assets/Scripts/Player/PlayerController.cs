using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 curInput;
    Rigidbody _rigidbody;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float jumpPower = 3f;
    [SerializeField] private LayerMask roadLayerMask;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = transform.forward * curInput.y + transform.right * curInput.x;
        direction *= _speed;
        direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
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

    bool OnGround()
    {
        Ray ray = new Ray(transform.position + (-transform.up * 0.4f), Vector3.down);
        Debug.Log(ray);
        return Physics.Raycast(ray, 0.1f, roadLayerMask);
    }
}
