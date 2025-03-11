using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapObject 상속 
public class JumpObject : MapObject
{
    // 점프대 점프 파워 
    [SerializeField] float jumpPower;
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어만 점프 시키기
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
}
