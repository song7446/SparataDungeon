using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // ü��
    [SerializeField] private UIState hp;
    // ������
    [SerializeField] private UIState energy;
    // ��Ȳ�� ���� ���� ������ ������ �׼�
    public Action returnPlus;
    // �޸��� �Ҹ�Ǵ� ������ ��
    float runEnegry = 0.1f;

    private void Update()
    {
        // �޸� ���� ������ �Ҹ� 
        if (CharacterManager.Instance.Player.playerController.isRun)
        {
            energy.MinusValue(runEnegry);
        }
        // �޸��� ���� ���� ������ ����
        else
        {
            energy.PlusValue(runEnegry);
        }
    }

    // ü�� ȸ��
    public void Heal(float amount)
    {
        hp.PlusValue(amount);
    }

    // �߰� ���� �Ŀ� ��� 
    public void GetJump(ItemDataConcumable itemData)
    {
        // �߰� ���� �Ŀ� �������� value�� ���� 
        CharacterManager.Instance.Player.playerController.ChangeJump(itemData.value);
        // �׼ǿ� ���� �Ŀ� �ǵ����� ���� 
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnJump;
    }

    // �߰� ���ǵ� ��� 
    public void GetSpeed(ItemDataConcumable itemData)
    {
        // �߰� ���ǵ� �������� value�� ���� 
        CharacterManager.Instance.Player.playerController.ChangeSpeed(itemData.value);
        // �׼ǿ� ���ǵ� �ǵ����� ���� 
        returnPlus = CharacterManager.Instance.Player.playerController.ReturnSpeed;
    }

    // ���� ������ ���
    public float GetEnergy()
    {
        return energy.GetValue();
    }
}
