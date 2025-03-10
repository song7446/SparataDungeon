using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // 체력
    [SerializeField] private UIState hp;
    // 에너지
    [SerializeField] private UIState energy;
    // 상황에 따라 원래 값으로 돌리는 액션
    public Action returnPlus;
    // 달릴때 소모되는 에너지 양
    float runEnegry = 0.1f;

    private void Update()
    {
        // 달릴 때는 에너지 소모 
        if (CharacterManager.Instance.Player.playerController.isRun)
        {
            energy.MinusValue(runEnegry);
        }
        // 달리지 않을 때는 에너지 충전
        else
        {
            energy.PlusValue(runEnegry);
        }
    }

    // 체력 회복
    public void Heal(float amount)
    {
        hp.PlusValue(amount);
    }

    // 추가 점프 파워 얻기 
    public void GetJump(ItemDataConcumable itemData)
    {
        // 추가 점프 파워 아이템의 value로 설정 
        CharacterManager.Instance.Player.playerController.ChangeJump(itemData.value);
        // 액션에 점프 파워 되돌리기 설정 
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnJump;
    }

    // 추가 스피드 얻기 
    public void GetSpeed(ItemDataConcumable itemData)
    {
        // 추가 스피드 아이템의 value로 설정 
        CharacterManager.Instance.Player.playerController.ChangeSpeed(itemData.value);
        // 액션에 스피드 되돌리기 설정 
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnSpeed;
    }

    // 현재 에너지 얻기
    public float GetEnergy()
    {
        return energy.GetValue();
    }
}
