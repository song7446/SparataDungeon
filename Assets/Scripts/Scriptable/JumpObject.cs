using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapObject ��� 
public class JumpObject : MapObject
{
    // ������ ���� �Ŀ� 
    [SerializeField] float jumpPower;
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ���� ��Ű��
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
}
