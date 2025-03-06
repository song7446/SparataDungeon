using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int hp;

    void ChangeHp(float amount)
    {
        hp += (int)amount;
    }
}
