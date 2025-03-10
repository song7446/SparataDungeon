using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    public Items item;
    public TextMeshProUGUI itemName;

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
