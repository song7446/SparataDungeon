using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    UISlot[] slots;
    Transform slotPanel;
    float mouseScrollY;
    int curidx = 0;

    private void Awake()
    {
        slots = new UISlot[11];
        slotPanel = transform.GetChild(0);
        for (int i = 0; i < 11; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<UISlot>();
        }
        slots[curidx].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
    }

    private void Update()
    {

    }

    public void OnInventorySelect(InputAction.CallbackContext context)
    {
        mouseScrollY = context.action.ReadValue<float>();
        if (mouseScrollY > 0)
        {
            curidx++;
        }
        else if (mouseScrollY < 0)
        {
            curidx--;
        }
        curidx = Mathf.Clamp(curidx, 0, 10);
        slots[curidx].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
    }

    public void PushItem(Items item)
    {
        if (slots[curidx].item == null)
        {
            slots[curidx].item = item;
            ChangeInventoryPrompt();
        }
        else
        {
            // 아이템 찬 상태 
        }
    }

    public void DropItem()
    {
        if (slots[curidx].item != null)
        {
            slots[curidx].item = null;
        }
    }

    void ChangeInventoryPrompt()
    {
        slots[curidx].ChangeItemPromt();
    }
}
