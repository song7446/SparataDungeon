using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    // 아이템 
    public Items item;
    // 아이콘 대체 아이템 이름 텍스트 
    public TextMeshProUGUI itemName;

    // 아이템 이름 띄워주는 함수
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
