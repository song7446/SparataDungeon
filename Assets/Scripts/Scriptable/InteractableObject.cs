using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맵의 길이나 건물 혹은 아이템이 상속
public interface InteractableObject
{
    // 이름 가져오기
    public string GetObjectName();

    // 설명 가져오기
    public string GetObjectDescription();
}
