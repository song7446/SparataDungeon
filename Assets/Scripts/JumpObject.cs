using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MapObject
{
    [SerializeField] float jumpPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
}
