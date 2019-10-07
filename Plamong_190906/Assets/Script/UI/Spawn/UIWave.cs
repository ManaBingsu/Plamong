using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWave : MonoBehaviour
{
    // 현재 웨이브 인덱스
    public Text waveText;
    // 최대 웨이브 인덱스
    public Text maxWaveText;

    private void Start()
    {
        StageManager.stageManager.EvWave += new StageManager.EventHandler(SetWave);
        SetWave();
    }

    void SetWave()
    {
        waveText.text = StageManager.stageManager.CurrentWave.waveIndex.ToString();
        maxWaveText.text = StageManager.stageManager.CurrentStage.waveDataList.Count.ToString();
    }
}
