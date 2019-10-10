using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UISkillInfo : MonoBehaviour
{
    // 싱글톤과 이벤트
    public static UISkillInfo skillInfo;
    public delegate void eventHandler();
    public eventHandler getInfoEvent;
    // Skill UI On / Off
    public GameObject objSkillUI;
    [Header("Select Skill")]
    // 현재 보고 있는 스킬
    [SerializeField]
    private UISkillTreeIcon selectSkill;
    [Header("Refernce")]
    // 바꿔야 하는 text 오브젝트들
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Text skillName;
    [SerializeField]
    private Text script;
    // 스킬 포인트 텍스트
    [SerializeField]
    private Text skillPoint;
    // 플레이어 데이터
    [SerializeField]
    private PlayerData playerData;

    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public RawImage screen;
    

    private void Awake()
    {
        if (skillInfo == null)
            skillInfo = this;
    }

    private void Start()
    {
        if (videoPlayer.clip == null)
            videoPlayer.gameObject.SetActive(false);
        playerData = PlayerController.player.playerData;
        // 스킬 포인트
        skillPoint.text = playerData.SkillPoint.ToString();
        playerData.EvSkillPoint += new PlayerData.EventHandler(SetSkillLevel);
        // 스킬정보창 띄우기 이벤트 할당
        KeySettingManager.keyManager.EvSkillInfo += new KeySettingManager.EventHandler(OnOffSkillInfo);
        StageManager.stageManager.EvIsRestEnd += new StageManager.EventHandler(ForceOffSkillInfo);
    }

    public void SetInfo(UISkillTreeIcon skillIcon, SkillData skillData)
    {
        if (videoPlayer.gameObject.activeSelf == false)
            videoPlayer.gameObject.SetActive(true);

        //스킬 아이콘 빛나게 하기
        if (selectSkill != null)
            selectSkill.skillIcon.color = Color.white;
        selectSkill = skillIcon;
        selectSkill.skillIcon.color = new Color(1, 1, 1, 0.5f);

        iconImage.sprite = skillData.icon;
        skillName.text = skillData.skillName;
        script.text = skillData.script;
        videoPlayer.clip = skillData.video;
    }

    public void SetInfoNull()
    {
        skillName.text = "-";
        script.text = "-";
        videoPlayer.clip = null;
        videoPlayer.gameObject.SetActive(false);
    }

    public void SetSkillLevel()
    {
        skillPoint.text = playerData.SkillPoint.ToString();
    }

    public void LearnSkill()
    {
        if(selectSkill != null)
        {
            // 선행스킬을 배우지 않았을 경우
            for (int i = 0; i < selectSkill.skillData.fowardSkillList.Count; i++)
            {
                if(selectSkill.skillData.fowardSkillList[i].isLearned == false)
                {
                    Debug.Log("선행스킬 부족!");
                    // 선행스킬 안찍음! 효과음
                    return;
                }
            }


            selectSkill.skillData.Level += 1;
        }
    }

    public void OnOffSkillInfo()
    {
        // 쉬는 시간 아닐경우 못함
        if (StageManager.stageManager.IsRest == false)
            return;

        if (objSkillUI.activeSelf)
        {
            objSkillUI.SetActive(false);
        }
        else
        {
            objSkillUI.SetActive(true);
        }
    }

    void ForceOffSkillInfo()
    {
        objSkillUI.SetActive(false);
    }
}
