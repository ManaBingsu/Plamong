using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    public Text stageText;

    private void Start()
    {
        StageManager.stageManager.EvStage += new StageManager.EventHandler(SetStage);
        SetStage();
    }

    void SetStage()
    {
        stageText.text = StageManager.stageManager.CurrentStage.stageIndex.ToString();
    }
}
