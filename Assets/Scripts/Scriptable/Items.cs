using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Items : MonoBehaviour, InteractableObject
{
    // ������ ������ 
    public ItemData itemData;

    // ������ �̸� ��������
    public string GetObjectName()
    {
        return itemData.displayName;
    }

    // ������ ���� �������� 
    public string GetObjectDescription()
    {
        return itemData.description;
    }

    // ������ ��� 
    public void UseItem()
    {
        switch (itemData.consumables[0].type)
        {
            // ü�� ����
            case ConsumableType.Health:
                CharacterManager.Instance.Player.playerState.Heal(itemData.consumables[0].value);
                break;
            // ������ ���� 
            case ConsumableType.Jump:
                CharacterManager.Instance.Player.playerState.GetJump(itemData.consumables[0]);
                TimeCheck(itemData.consumables[0]);
                break;
            // ���ǵ� ���� 
            case ConsumableType.Speed:
                CharacterManager.Instance.Player.playerState.GetSpeed(itemData.consumables[0]);
                TimeCheck(itemData.consumables[0]);
                break;
        }
    }

    // ������ ���� �ð��� 
    public void ChangeItemTime(float time)
    {
        UIManager.Instance.itemTime.text = time.ToString("N2");
    }

    public void TimeCheck(ItemDataConcumable itemData)
    {
        UIManager.Instance.itemTime.gameObject.SetActive(true);
        StartCoroutine(StopWatch(itemData));
    }

    // �������� ���ӽð����� 0���� �ð� ��� ��Ȱ��ȭ �ڷ�ƾ
    IEnumerator StopWatch(ItemDataConcumable itemData)
    {
        float time = itemData.time;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            ChangeItemTime(time);
            yield return null;
        }
        CharacterManager.Instance.Player.playerState.returnPlus?.Invoke();
        UIManager.Instance.itemTime.gameObject.SetActive(false);
        yield return null;
    }
}
