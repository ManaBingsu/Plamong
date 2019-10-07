using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaboratoyData", menuName = "Laboratory/LaboratoryData")]
public class LaboratoryData : ScriptableObject
{
    public delegate void EventHandler();

    // 연구실 현재 페이즈
    [SerializeField]
    private int currentPhase;
    public event EventHandler EvPhase;
    public int CurrentPhase
    {
        get { return currentPhase; }
        set
        {
            currentPhase = value;
            EvPhase();
        }
    }

    [Header("Phase List")]
    // 페이즈 목록
    public List<Phase> phaseList;

    [Header("Stat")]
    //연구실 최대 체력
    [SerializeField]
    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
        }
    }
    [SerializeField]
    private int phaseMaxHp;
    public int PhaseMaxHP
    {
        get { return phaseMaxHp; }
        set
        {
            phaseMaxHp = value;
        }
    }
    //연구실 체력 
    [SerializeField]
    private int hp;
    public event EventHandler EvHP;
    public int HP
    {
        get { return hp; }
        set
        {
            if (hp < 1)
                return;

            if (value <= phaseList[CurrentPhase + 1].phaseMaxHp)
            {
                CurrentPhase++;
                PhaseMaxHP = phaseList[CurrentPhase].phaseMaxHp;
            }
            hp = value;
            EvHP();
        }
    }
}

[System.Serializable]
public struct Phase
{
    public int phaseMaxHp;
    public float bonusAbility;
}
