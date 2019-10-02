using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTurretGun : AbsSkill
{
    // 소환할 대상
    public GameObject turret;
    // 앞의 땅 체크
    public TurretChecker turretChecker;
    // 스폰 할 거리
    public float spawnDistance;

    public override void ActivateSkill()
    {
        if (!skillData.isLearned)
        {
            // 배우지 않음!
            return;
        }
        CheckSpawnTurret();
    }

    void CheckSpawnTurret()
    {
        // 자원이 가능한지 확인
        TurretData tData = turret.GetComponent<AbsTurret>().turretData;
        if (PlayerController.player.playerData.Titanium - tData.Titanium < 0)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Plane");
        Vector3 targetPos;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.tag == "Ground")
        {
            targetPos = hit.point;

            Transform pTransform = PlayerController.player.transform;
            Vector3 playerPos = new Vector3(pTransform.position.x, 0f, pTransform.position.z);
            Vector3 mousePos = targetPos;
            mousePos.y = 0f;
            Vector3 direction = playerPos + (mousePos - playerPos).normalized * spawnDistance;

            turretChecker.gameObject.SetActive(true);
            turretChecker.transform.position = direction;
        }

    }
}
