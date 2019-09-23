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
            level = value;
            EvLevel();
        }
    }

    [TextArea]
    public string script;
}
