using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public static DummyPlayer player;

    // 플레이어 데이터
    public PlayerData playerData;

    private void Awake()
    {
        if (player == null)
            player = this;
    }
}
