using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILaboratory : MonoBehaviour
{
    // 현재 연구실 페이즈
    private int phaseNum;
    public Text phaseText;
    public Image phaseHPBar;
    public Image phaseHPEffectBar;
    // 페이즈에 따라 갱신
    private int maxHP;

    private void Start()
    {
        LaboratoryInfo.laboratory.laboratoryData.EvPhase += new LaboratoryData.EventHandler(SetPhase);
        LaboratoryInfo.laboratory.laboratoryData.EvHP += new LaboratoryData.EventHandler(DisplayHP);
        // 페이즈 넘버
        SetPhase();
    }

    void SetPhase()
    {
        phaseNum = LaboratoryInfo.laboratory.laboratoryData.CurrentPhase;
        // 페이즈 인덱스 출력
        phaseText.text = phaseNum.ToString();
    }

    void DisplayHP()
    {
        phaseHPBar.fillAmount = (float)LaboratoryInfo.laboratory.laboratoryData.HP / LaboratoryInfo.laboratory.laboratoryData.MaxHP;
        StartCoroutine(EffectHP());
    }

    IEnumerator EffectHP()
    {
        while (phaseHPEffectBar.fillAmount > phaseHPBar.fillAmount)
        {
            phaseHPEffectBar.fillAmount -= 0.005f;
            yield return null;
        }
    }
}
