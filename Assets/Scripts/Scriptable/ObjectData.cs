using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "New Object")]
public class ObjectData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
}