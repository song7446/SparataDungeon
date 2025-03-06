using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    Image hpBarImage;
    
    void UpdateHpBar()
    {
        hpBarImage.fillAmount = 1f;
    }
}
