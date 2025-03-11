using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ Ÿ��
public enum ItemType
{
    // ��� ���� 
    Useable,
    // �Ҹ� ����
    Consumable,
}

// �Ҹ�� ȸ�� Ÿ��
public enum ConsumableType
{
    // ü��
    Health,
    // ���� �Ŀ�
    Jump,
    // �ӵ�
    Speed,
}

[Serializable]
public class ItemDataConcumable
{
    // �Ҹ�� ȸ�� Ÿ��
    public ConsumableType type;
    // ȸ����
    public float value;
    // ���ӽð�
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
