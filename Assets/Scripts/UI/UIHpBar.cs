using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    Image hpBarImage;

    private void Awake()
    {
        hpBarImage = GetComponent<Image>();
    }

    private void UpdateHpBar(float amount)
    {
        hpBarImage.fillAmount = amount;
    }
}
