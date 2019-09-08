using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    
    // 플레이어의 데이터
    public PlayerData playerData;

    private void Awake()
    {
        if (player == null)
            player = this;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0.005f, 0f, 0f);
    }

    private void Start()
    {
        LoadPlayerData();
    }
    // 플레이어의 데이터를 게임매니저로부터 불러와 clone화 시킴
    void LoadPlayerData()
    {
        playerData = Instantiate(GameManager.gameManager.playerInfoData) as PlayerData;
    }
    // 플레이어 움직임 제어
    void Move()
    {

    }
    // 플레이어 공격 제어
    void MouseAttack()
    {

    }
}
