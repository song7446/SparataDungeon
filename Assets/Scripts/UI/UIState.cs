using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UIState : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public Image barImage;
    
    private void Update()
    {
        barImage.fillAmount = BarPercentage();
    }

    float BarPercentage()
    {
        return curValue / maxValue;
    }

    public void plusValue(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void minusValue(float amount) 
    {
        curValue = Mathf.Max(curValue - amount, 0);
    }
}
