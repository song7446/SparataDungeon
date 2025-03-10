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

    // hp ��
    public UIState uiHp;
    // ��������
    public UIState uiEnergy;
    // �κ��丮
    public UIInventory UIInventory;
    // ������ ���ѽð�
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
