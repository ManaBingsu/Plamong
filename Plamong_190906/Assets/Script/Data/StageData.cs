using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawning", menuName = "Spawning/StageData")]
public class StageData : ScriptableObject
{
    // 스테이지 쉬는 시간
    public float stageRestTime;
    public List<WaveData> waveDataList;
}
