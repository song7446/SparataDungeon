using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject().AddComponent<UIManager>();
            }
            return _instance;
        }
    }

    // hp 바
    public UIState uiHp;
    // 에너지바
    public UIState uiEnergy;
    // 인벤토리
    public UIInventory UIInventory;
    // 아이템 제한시간
    public TextMeshProUGUI itemTime;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
        itemTime.gameObject.SetActive(false);
    }
}
