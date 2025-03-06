using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField] ObjectData objectdata;

    public string GetObjectName()
    {
        return objectdata.displayName;
    }

    public string GetObjectDescription()
    {
        return objectdata.description;
    }
}
