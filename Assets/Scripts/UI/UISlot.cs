using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    // ������ 
    public Items item;
    // ������ ��ü ������ �̸� �ؽ�Ʈ 
    public TextMeshProUGUI itemName;

    // ������ �̸� ����ִ� �Լ�
    public void ChangeItemPromt()
    {
        if (item != null)
        {
            itemName.text = item.GetObjectName();
        }
        else
        {
            itemName.text = "";
        }
    }
}
