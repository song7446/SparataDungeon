using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private UIState hp;
    [SerializeField] private UIState energy;
    float runEnegry = 0.1f;

    private void Update()
    {
        if (CharacterManager.Instance.Player.playerController.isRun)
        {
            energy.minusValue(runEnegry);
        }
        else
        {
            energy.plusValue(runEnegry);
        }
    }

    public void Heal(float amount)
    {
        hp.plusValue(amount);
    }

    public void GetEnergy(float amount)
    {
        energy.plusValue(amount);
    }

    public void GetSpeed(float amount)
    {
        CharacterManager.Instance.Player.playerController.ChangeSpeed(amount);
    }
}
