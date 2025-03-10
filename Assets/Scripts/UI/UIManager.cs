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

    public UIState uiHp;
    public UIState uiEnergy;
    public UIInventory UIInventory;
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

    public void ChangeItemTime(float time)
    {
        itemTime.text = time.ToString("N2");
    }

    public void TimeCheck(ItemDataConcumable itemData)
    {
        itemTime.gameObject.SetActive(true);
        StartCoroutine(StopWatch(itemData));        
    }

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
