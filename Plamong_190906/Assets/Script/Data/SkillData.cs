using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public delegate void EventHandler();

    public enum Type { Weapon, Turret, Reinforcement, Special }
    public Type weaponType;

    [Header("Info")]
    public Sprite icon;
    public string skillName;
    public int index;
    public VideoClip video;
    [Header("Acitve")]
    public bool isLearned;

    public List<SkillData> fowardSkillList;
    // 스킬 레벨
    public event EventHandler EvLevel;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            if (value > maxLevel)
            {
                Debug.Log("스킬 레벨 최대치입니다!");
                //이미 최대치입니다 이벤트!
                return;
            }

            if(PlayerController.player.playerData.SkillPoint > 0)
            {
                PlayerController.player.playerData.SkillPoint -= 1;
                level = value;
                isLearned = true;
                Debug.Log("스킬 " + skillName + " 레벨 " + level + " 업!");
                //EvLevel(); 레벨업 이벤트!
            }
            else
            {
                // 스킬 포인트가 부족합니다! 이벤트
                Debug.Log("스킬 포인트가 부족합니다!");
            }        

        }
    }

    [TextArea]
    public string script;
}
