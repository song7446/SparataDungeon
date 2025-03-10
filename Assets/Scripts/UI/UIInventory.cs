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
    // �κ��丮 ����
    [SerializeField] UISlot[] slots;
    // ���� �θ�
    Transform slotPanel;
    // ���� ���ÿ� ���콺 �� �Է�
    float mouseScrollY;
    // ���� �ε��� 
    int curidx = 0;

    private void Awake()
    {
        // ���� �ʱ�ȭ 
        slots = new UISlot[11];
        slotPanel = transform.GetChild(0);
        for (int i = 0; i < 11; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<UISlot>();
        }
        // ���õ� ���� �ƿ����� �� ���� 
        slots[curidx].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
    }

    // �κ��丮 ���� ���콺 �� �Է�
    public void OnInventorySelect(InputAction.CallbackContext context)
    {
        mouseScrollY = context.action.ReadValue<float>();
        // �� ������ �ε��� ���� 
        if (mouseScrollY < 0)
        {
            curidx++;
        }
        // �� �ø��� �ε��� ���� 
        else if (mouseScrollY > 0)
        {
            curidx--;
        }
        // �ִ� �ּ� ���� ������ �ʰ� ���� 
        curidx = Mathf.Clamp(curidx, 0, 10);

        // ���õ� ���� �ƿ����� �� ���� 
        InventorySelectColorChange();
    }

    // ������ ������ Q
    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DropItem();
        }
    }

    // ������ ��� ���콺 ���� Ŭ��
    public void OnUseItem(InputAction.CallbackContext context)
    {
        // ���� ���õ� ���Կ� �������� ���� ��쿡��
        if (context.phase == InputActionPhase.Started && slots[curidx].item != null)
        {
            slots[curidx].item.UseItem();
            slots[curidx].item = null;
            ChangeInventoryPrompt();
        }
    }

    // ���õ� ������ ������ �ƴ� ������ ��� �Ͼ������ �ƿ������� ����
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

    // ���Կ� ������ �ֱ� 
    public void PushItem(Items item)
    {
        // ���õ� ���Կ� �������� �ִٸ� ������
        DropItem();

        // ������ ���� 
        Items copyItem = gameObject.AddComponent<Items>();
        copyItem.itemData = item.itemData;
        slots[curidx].item = copyItem;
        ChangeInventoryPrompt();
    }

    // ������ ������
    public void DropItem()
    {
        // null ���� - ���õ� ���Կ� �������� �ִٸ�  
        if (slots[curidx].item != null)
        {
            // ������ ���� �� ����߸���
            GameObject dropItem = Instantiate(slots[curidx].item.itemData.dropPrefab, 
                CharacterManager.Instance.Player.playerController.transform.forward
                + CharacterManager.Instance.Player.playerController.cameraContainer.up, Quaternion.identity);
            slots[curidx].item = null;
        }
        ChangeInventoryPrompt();
    }

    // �κ��丮 ������Ʈ ���� - �������� ��� ������ �̸� �ؽ�Ʈ�� ��ü
    void ChangeInventoryPrompt()
    {
        slots[curidx].ChangeItemPromt();
    }
}
