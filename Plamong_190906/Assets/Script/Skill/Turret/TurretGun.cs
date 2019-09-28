using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : AbsTurret
{
    [SerializeField]
    private List<Transform> enemyList;

    [SerializeField]
    private float targetCheckDelay;
    private WaitForSeconds targetCheckDelayTime;

    [SerializeField]
    private BulletPooling bulletPooling;

    private void Start()
    {
        StartCoroutine(SetTarget());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!enemyList.Contains(col.transform) && col.CompareTag("Enemy"))
        {
            enemyList.Add(col.transform);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (enemyList.Contains(col.transform) && col.CompareTag("Enemy"))
        {
            enemyList.Remove(col.transform);
        }
    }

    IEnumerator SetTarget()
    {
        targetCheckDelayTime = new WaitForSeconds(turretData.AttackDelay);

        while (true)
        {
            if (enemyList.Count > 0)
            {
                float minDistance = 9999f;
                foreach (Transform enemy in enemyList)
                {
                    if(enemy != null)
                    {
                        float targetDistance = GetTargetDistance(enemy);
                        if (targetDistance < minDistance)
                        {
                            minDistance = targetDistance;
                            target = enemy;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
            }
            else
            {
                target = null;
            }

            if(target != null)
            {
                RotateGun();
                AttackTarget();
            }

            yield return targetCheckDelayTime;
        }
    }

    void AttackTarget()
    {
        bulletPooling.ShotBullet
            (turretData.Damage, transform, target.position, 50f,
            BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Straight,
            AbsEnemy.CrowdControl.KnockBack, 1.5f, 10f);
    }

    void RotateGun()
    {
        Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 v = (targetPos - myPos).normalized;

        float direction = Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;

        gunTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, direction));
    }

    public float GetTargetDistance(Transform tg)
    {
        Vector3 targetPos = new Vector3(tg.position.x, 0f, tg.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);

        float distance = Vector3.Distance(myPos, targetPos);
        return distance;
    }

}
