using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public enum TurretName { GunTurret }
    public enum SwordName { Strike }
    public enum GunName { GunTurret }

    public TurretChecker turretChecker;
    public List<GameObject> turretList;
    public float spawnDistance;

    public List<AbsSkill> skillList;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            ActivateSkill(0);
        }
    }

    void ActivateSkill(int num)
    {
        skillList[num].ActivateSkill();
    }

    void CheckSpawnTurret(int index)
    {
        // 자원이 가능한지 확인
        TurretData tData = turretList[index].GetComponent<AbsTurret>().turretData;
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

    public void SpawnDistance(Vector3 pos, int index)
    {
        if (turretChecker.isBuildPos)
        {
            Instantiate(turretList[index], pos, Quaternion.identity);
        }

        turretChecker.gameObject.SetActive(false);
    }
}
