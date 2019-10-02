using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaboratoyData", menuName = "Laboratory/LaboratoryData")]
public class LaboratoryData : ScriptableObject
{
    // 페이즈 목록
    public List<Phase> phaseList;

    //연구실 최대 체력
    [SerializeField]
    private int maxHp;
    public int MaxHP
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }
    //연구실 체력 
    [SerializeField]
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }
}

[System.Serializable]
public struct Phase
{
    public int phaseMaxHp;
    public float bonusAbility;
}
