using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        if (mouseScrollY < 0)
        {
            curidx++;
        }
        else if (mouseScrollY > 0)
        {
            curidx--;
        }
        curidx = Mathf.Clamp(curidx, 0, 10);
        InventorySelectColorChange();
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DropItem();
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && slots[curidx].item != null)
        {
            slots[curidx].item.UseItem();
            slots[curidx].item = null;
        }
    }

    void InventorySelectColorChange()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == curidx)
            {
                slots[i].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.white;
            }
        }
    }

    public void PushItem(Items item)
    {
        DropItem();
        slots[curidx].item = item;
        ChangeInventoryPrompt();
    }

    public void DropItem()
    {
        if (slots[curidx].item != null)
        {
            GameObject dropItem = Instantiate(slots[curidx].item.itemData.dropPrefab, CharacterManager.Instance.Player.transform.position + Vector3.up + Vector3.forward, Quaternion.identity);
            slots[curidx].item = null;
        }
        ChangeInventoryPrompt();
    }

    void ChangeInventoryPrompt()
    {
        slots[curidx].ChangeItemPromt();
    }
}
