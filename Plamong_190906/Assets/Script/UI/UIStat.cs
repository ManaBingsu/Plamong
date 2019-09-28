using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStat : MonoBehaviour
{
    // 플레이어의 데이터 참조
    [SerializeField]
    private PlayerData playerData;

    public Image DuraBar;
    public Image EffectDuraBar;
    public Image PowerBar;
    public Image EffectPowerBar;
    public Image TitanBar;
    public Image EffectTitanBar;

    private void Start()
    {
        StartCoroutine(LoadPlayerData());
    }

    IEnumerator LoadPlayerData()
    {
        yield return null;
        playerData = PlayerController.player.playerData;

        playerData.EvDurability += new PlayerData.EventHandler(DisplayDurability);
        playerData.EvPower += new PlayerData.EventHandler(DisplayPower);
        playerData.EvTitanium += new PlayerData.EventHandler(DisplayTitanium);
    }

    // HP BAR
    void DisplayDurability()
    {
        DuraBar.fillAmount = (float)playerData.Durability / playerData.MaxDurability;
        StartCoroutine(EffectDurability());
    }

    IEnumerator EffectDurability()
    {
        while(EffectDuraBar.fillAmount > DuraBar.fillAmount)
        {
            EffectDuraBar.fillAmount -= 0.005f;
            yield return null;
        }
    }
    // POWER BAR
    void DisplayPower()
    {
        DuraBar.fillAmount = (float)playerData.Power / playerData.MaxPower;
        StartCoroutine(EffectPower());
    }

    IEnumerator EffectPower()
    {
        while (EffectPowerBar.fillAmount > PowerBar.fillAmount)
        {
            EffectPowerBar.fillAmount -= 0.005f;
            yield return null;
        }
    }

    // TITAN BAR
    void DisplayTitanium()
    {
        TitanBar.fillAmount = (float)playerData.Titanium / playerData.MaxTitanium;
        StartCoroutine(EffectTitanium());
    }

    IEnumerator EffectTitanium()
    {
        while (EffectTitanBar.fillAmount > TitanBar.fillAmount)
        {
            EffectTitanBar.fillAmount -= 0.005f;
            yield return null;
        }
    }

}
