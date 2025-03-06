using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 curInput;
    Rigidbody _rigidbody;
    [SerializeField] private float _speed = 3f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
}
