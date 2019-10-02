using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTreeIcon : MonoBehaviour
{
    public SkillData skillData;
    // 원본 스킬 참조
    public AbsSkill originSkill;
    public Image skillIcon;

    private void Start()
    {
        // 스킬 컨테이너 참조
        skillData = originSkill.skillData;
        // 스킬 아이콘 할당
        skillIcon.sprite = skillData.icon;
        // 스킬 이벤트 할당
    }

    // 스킬 아이콘 누를시
    public void SetInfo()
    {
        UISkillInfo.skillInfo.SetInfo(this, skillData);
    }
    /*
    public void LearnSkill()
    {
        if (PlayerController.player.playerData.SkillPoint > 0)
        {
            originSkill.IsLearn = true;
            Debug.Log(originSkill.IsLearn);
            PlayerController.player.playerData.SkillPoint -= 1;
            skillData.Level += 1;
        }
    }*/
}
