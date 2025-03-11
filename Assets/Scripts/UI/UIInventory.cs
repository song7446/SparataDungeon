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
    // 인벤토리 슬롯
    [SerializeField] UISlot[] slots;
    // 슬롯 부모
    Transform slotPanel;
    // 슬롯 선택용 마우스 휠 입력
    float mouseScrollY;
    // 슬롯 인덱스 
    int curidx = 0;

    private void Awake()
    {
        // 슬롯 초기화 
        slots = new UISlot[11];
        slotPanel = transform.GetChild(0);
        for (int i = 0; i < 11; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<UISlot>();
        }
        // 선택된 슬롯 아웃라인 색 변경 
        slots[curidx].transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
    }

    // 인벤토리 선택 마우스 휠 입력
    public void OnInventorySelect(InputAction.CallbackContext context)
    {
        mouseScrollY = context.action.ReadValue<float>();
        // 휠 내리면 인덱스 증가 
        if (mouseScrollY < 0)
        {
            curidx++;
        }
        // 휠 올리면 인덱스 감소 
        else if (mouseScrollY > 0)
        {
            curidx--;
        }
        // 최대 최소 범위 나가지 않게 설정 
        curidx = Mathf.Clamp(curidx, 0, 10);

        // 선택된 슬롯 아웃라인 색 변경 
        InventorySelectColorChange();
    }

    // 아이템 버리기 Q
    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DropItem();
        }
    }

    // 아이템 사용 마우스 왼쪽 클릭
    public void OnUseItem(InputAction.CallbackContext context)
    {
        // 현재 선택된 슬롯에 아이템이 있을 경우에만
        if (context.phase == InputActionPhase.Started && slots[curidx].item != null)
        {
            slots[curidx].item.UseItem();
            slots[curidx].item = null;
            ChangeInventoryPrompt();
        }
    }

    // 선택된 슬롯은 빨간색 아닌 슬롯은 모두 하얀색으로 아웃라인을 변경
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

    // 슬롯에 아이템 넣기 
    public void PushItem(Items item)
    {
        // 선택된 슬롯에 아이템이 있다면 버리기
        DropItem();

        // 아이템 복사 
        Items copyItem = gameObject.AddComponent<Items>();
        copyItem.itemData = item.itemData;
        slots[curidx].item = copyItem;
        ChangeInventoryPrompt();
    }

    // 아이템 버리기
    public void DropItem()
    {
        // null 방지 - 선택된 슬롯에 아이템이 있다면  
        if (slots[curidx].item != null)
        {
            // 아이템 복사 후 떨어뜨리기
            GameObject dropItem = Instantiate(slots[curidx].item.itemData.dropPrefab, 
                CharacterManager.Instance.Player.playerController.transform.forward
                + CharacterManager.Instance.Player.playerController.cameraContainer.up, Quaternion.identity);
            slots[curidx].item = null;
        }
        ChangeInventoryPrompt();
    }

    // 인벤토리 프롬프트 변경 - 아이콘이 없어서 아이템 이름 텍스트로 대체
    void ChangeInventoryPrompt()
    {
        slots[curidx].ChangeItemPromt();
    }
}
