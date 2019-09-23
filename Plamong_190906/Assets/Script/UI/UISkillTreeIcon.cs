using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTreeIcon : MonoBehaviour
{
    [SerializeField]
    private SkillData skillData;

    public Image skillIcon;

    private void Start()
    {
        skillIcon.sprite = skillData.icon;
    }

    // 스킬 아이콘 누를시
    public void SetInfo()
    {
        UISkillInfo.skillInfo.SetInfo(this, skillData);
    }
}
