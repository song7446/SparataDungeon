using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UIState : MonoBehaviour
{
    // 현재 값
    public float curValue;
    // 최대 값
    public float maxValue;
    // UI 바 갱신용 이미지
    public Image barImage;
    
    private void Update()
    {
        barImage.fillAmount = BarPercentage();
    }

    // UI 바 채워지는 퍼센트 계산 
    float BarPercentage()
    {
        return curValue / maxValue;
    }

    // 값 더하기
    public void PlusValue(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // 값 빼기 
    public void MinusValue(float amount) 
    {
        curValue = Mathf.Max(curValue - amount, 0);
    }

    // 현재 값 불러오기 용 
    public float GetValue()
    {
        return curValue;
    }
}
