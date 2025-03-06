using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergyBar : MonoBehaviour
{
    Image energyBarImage;

    private void Awake()
    {
        energyBarImage = GetComponent<Image>();
    }

    public void UpdateHpBar(float amount)
    {
        energyBarImage.fillAmount += amount;
    }
}
