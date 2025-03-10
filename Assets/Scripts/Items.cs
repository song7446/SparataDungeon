using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour, InteractableObject
{
    public ItemData itemData;
    public string GetObjectName()
    {
        return itemData.displayName;
    }

    public string GetObjectDescription()
    {
        return itemData.description;
    }

    public void UseItem()
    {
        switch (itemData.consumables[0].type)
        {
            case ConsumableType.Health:
                CharacterManager.Instance.Player.playerState.Heal(itemData.consumables[0].value);
                break;
            case ConsumableType.Jump:
                CharacterManager.Instance.Player.playerState.GetJump(itemData.consumables[0]);
                UIManager.Instance.TimeCheck(itemData.consumables[0]);
                break;
            case ConsumableType.Speed:
                CharacterManager.Instance.Player.playerState.GetSpeed(itemData.consumables[0]);
                UIManager.Instance.TimeCheck(itemData.consumables[0]);
                break;
        }
    }
}
