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

    // 아이템 제한 시간용 
    public void ChangeItemTime(float time)
    {
        itemTime.text = time.ToString("N2");
    }

    public void TimeCheck(ItemDataConcumable itemData)
    {
        itemTime.gameObject.SetActive(true);
        StartCoroutine(StopWatch(itemData));        
    }

    // 아이템의 지속시간부터 0까지 시간 재고 비활성화 코루틴
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
