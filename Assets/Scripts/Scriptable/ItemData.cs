using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 타입
public enum ItemType
{
    // 사용 가능 
    Useable,
    // 소모 가능
    Consumable,
}

// 소모시 회복 타입
public enum ConsumableType
{
    // 체력
    Health,
    // 점프 파워
    Jump,
    // 속도
    Speed,
}

[Serializable]
public class ItemDataConcumable
{
    // 소모시 회복 타입
    public ConsumableType type;
    // 회복량
    public float value;
    // 지속시간
    public float time;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stack")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConcumable[] consumables;
}
