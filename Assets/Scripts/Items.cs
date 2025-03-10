using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour, InteractableObject
{
    // 아이템 데이터 
    public ItemData itemData;

    // 아이템 이름 가져오기
    public string GetObjectName()
    {
        return itemData.displayName;
    }

    // 아이템 설명 가져오기 
    public string GetObjectDescription()
    {
        return itemData.description;
    }

    // 아이템 사용 
    public void UseItem()
    {
        switch (itemData.consumables[0].type)
        {
            // 체력 충전
            case ConsumableType.Health:
                CharacterManager.Instance.Player.playerState.Heal(itemData.consumables[0].value);
                break;
            // 점프력 증가 
            case ConsumableType.Jump:
                CharacterManager.Instance.Player.playerState.GetJump(itemData.consumables[0]);
                UIManager.Instance.TimeCheck(itemData.consumables[0]);
                break;
            // 스피드 증가 
            case ConsumableType.Speed:
                CharacterManager.Instance.Player.playerState.GetSpeed(itemData.consumables[0]);
                UIManager.Instance.TimeCheck(itemData.consumables[0]);
                break;
        }
    }
}
