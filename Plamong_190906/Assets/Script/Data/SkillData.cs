using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public enum Type { Weapon, Turret, Reinforcement, Special }
    public Type weaponType;

    [Header("Info")]
    public Sprite icon;
    public string skillName;
    public int index;

    [TextArea]
    public string script;
}
