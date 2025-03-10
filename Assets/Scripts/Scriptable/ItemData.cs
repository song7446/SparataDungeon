using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable,
    Consumable,
}

public enum ConsumableType
{
    Health,
    Jump,
    Speed,
}

[Serializable]
public class ItemDataConcumable
{
    public ConsumableType type;
    public float value;
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
