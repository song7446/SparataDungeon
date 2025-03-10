using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour,InteractableObject
{
    public ItemData itemData;
    public string GetObjectName()
    {
        return itemData.displayName;
    }

    public string GetObjectDescription()
    {
        return itemData.description;
    }

}
