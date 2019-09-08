using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager gameManager;
    // 플레이어의 기본 정보가 담긴 데이터
    public PlayerData playerInfoData;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
}
