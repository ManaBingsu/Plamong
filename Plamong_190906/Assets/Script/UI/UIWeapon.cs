using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private List<RawImage> weaponList;


    private void Start()
    {
        player = PlayerController.player;
        // 무기 변경 이벤트 추가
        player.EvWeapon += new PlayerController.EventHandler(ChangeCurrentWeapon);
        
    }
    // 착용 무기에 따라 ui 변경
    void ChangeCurrentWeapon()
    {
        if(player.weaponMode == PlayerController.WeaponMode.Sword)
        {
            weaponList[1].gameObject.SetActive(false);
            weaponList[0].gameObject.SetActive(true);
        }
        else
        {
            weaponList[0].gameObject.SetActive(false);
            weaponList[1].gameObject.SetActive(true);
        }
    }
}
