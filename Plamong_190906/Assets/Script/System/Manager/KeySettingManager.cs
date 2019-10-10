using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySettingManager : MonoBehaviour
{
    // 싱글톤
    public static KeySettingManager keyManager;

    public delegate void EventHandler();
    // keyData
    public KeySettingData keyData;

    // SkillInfo
    public event EventHandler EvSkillInfo;

    private void Awake()
    {
        if (keyManager == null)
            keyManager = this;

        keyData = Instantiate(keyData) as KeySettingData;
    }

    private void Update()
    {
        // 스킬 창
        if(Input.GetKeyUp(keyData.KeySkillInfo))
        {
            EvSkillInfo?.Invoke();
        }
    }
}
