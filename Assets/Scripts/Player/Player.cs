using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 컨트롤러
    public PlayerController playerController;
    // 플레이어 상태 
    public PlayerState playerState;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        playerState = GetComponent<PlayerState>();
    }
}
