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
    public Image TitanBar;

    private void Start()
    {
        StartCoroutine(LoadPlayerData());
    }

    IEnumerator LoadPlayerData()
    {
        yield return null;
        playerData = PlayerController.player.playerData;

        playerData.EvDurability += new PlayerData.EventHandler(DisplayDurability);
    }

    void DisplayDurability()
    {
        DuraBar.fillAmount = (float)playerData.Durability / playerData.MaxDurability;
        StartCoroutine(EffectDurability());
    }

    IEnumerator EffectDurability()
    {
        Debug.Log("z");
        while(EffectDuraBar.fillAmount > DuraBar.fillAmount)
        {
            EffectDuraBar.fillAmount -= 0.005f;
            yield return null;
        }
    }
}
