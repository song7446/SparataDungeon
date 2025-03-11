using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour, InteractableObject
{
    // 맵 오브젝트들 데이터 
    [SerializeField] ObjectData objectdata;

    // 이름 가져오기 
    public string GetObjectName()
    {
        return objectdata.displayName;
    }

    // 설명 가져오기 
    public string GetObjectDescription()
    {
        return objectdata.description;
    }
}
