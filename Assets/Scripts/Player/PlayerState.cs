using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private UIState hp;
    [SerializeField] private UIState energy;
    public Action returnPlus;
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

    public void GetJump(ItemDataConcumable itemData)
    {
        CharacterManager.Instance.Player.playerController.ChangeJump(itemData.value);
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnJump;
    }

    public void GetSpeed(ItemDataConcumable itemData)
    {
        CharacterManager.Instance.Player.playerController.ChangeSpeed(itemData.value);
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnSpeed;
    }
}
