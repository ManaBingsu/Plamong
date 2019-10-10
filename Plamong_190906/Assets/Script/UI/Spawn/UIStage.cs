using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    public Text stageText;
    public Text restText;
    public Text restTimerText;

    private void Start()
    {
        StageManager.stageManager.EvStage += new StageManager.EventHandler(SetStage);
        StageManager.stageManager.EvIsRest += new StageManager.EventHandler(SetRestText);
        StageManager.stageManager.EvRestTime += new StageManager.EventHandler(SetRestTimer);
        SetStage();
        SetRestText();
        SetRestTimer();
    }

    void SetStage()
    {
        stageText.text = StageManager.stageManager.CurrentStage.stageIndex.ToString();
    }

    void SetRestText()
    {
        if (StageManager.stageManager.IsRest)
            restText.text = "Rest Time";
        else
            restText.text = "Rush Time";
    }

    void SetRestTimer()
    {
        if(StageManager.stageManager.RestTime == 0)
        {
            restTimerText.text = null;
        }
        else
        {
            restTimerText.text = StageManager.stageManager.RestTime.ToString();
        }
    }
}
