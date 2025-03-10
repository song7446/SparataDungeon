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

    // ������ ���� �ð��� 
    public void ChangeItemTime(float time)
    {
        itemTime.text = time.ToString("N2");
    }

    public void TimeCheck(ItemDataConcumable itemData)
    {
        itemTime.gameObject.SetActive(true);
        StartCoroutine(StopWatch(itemData));        
    }

    // �������� ���ӽð����� 0���� �ð� ��� ��Ȱ��ȭ �ڷ�ƾ
    IEnumerator StopWatch(ItemDataConcumable itemData)
    {
        float time = itemData.time;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            ChangeItemTime(time);
            yield return null;
        }
        CharacterManager.Instance.Player.playerState.returnPlus?.Invoke();
        itemTime.gameObject.SetActive(false);
        yield return null;
    }
}
