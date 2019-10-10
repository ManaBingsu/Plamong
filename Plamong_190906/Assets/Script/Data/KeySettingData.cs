using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyData", menuName = "KeySetting/KeyData")]
public class KeySettingData : ScriptableObject
{
    [Header("UI")]
    [SerializeField]
    private KeyCode keySkillInfo;
    public KeyCode KeySkillInfo
    {
        get { return keySkillInfo; }
        set
        {
            keySkillInfo = value;
        }
    }

}
