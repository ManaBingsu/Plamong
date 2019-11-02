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

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SetTarget());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!enemyList.Contains(col.transform) && col.CompareTag("Enemy"))
        {
            enemyList.Add(col.transform);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            float distance = GetTargetDistance(col.transform);
            if(distance < turretData.InteractDistance)
            {
                if(Input.GetKeyUp(KeyCode.BackQuote))
                {
                    // 분해
                    if(cortnDecomp == null)
                    {
                        cortnDecomp = StartCoroutine(Decomposition());
                    }
                    else
                    // 분해 취소
                    {
                        StopCoroutine(cortnDecomp);
                        cortnDecomp = null;
                    }
                }
            }
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
            // 만약 상호작용중일경우
            if (isDecomp)
                continue;

            if (enemyList.Count > 0)
            {
                float minDistance = 9999f;
                for(int i = 0; i < enemyList.Count; i++)
                {
                    if(enemyList[i] != null)
                    {
                        float targetDistance = GetTargetDistance(enemyList[i]);
                        if (targetDistance < minDistance)
                        {
                            minDistance = targetDistance;
                            target = enemyList[i];
                        }
                    }
                    else
                    {
                        enemyList.Remove(enemyList[i]);
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
            AbsEnemy.CrowdControl.Slow, 0.5f, 0.5f);
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

    public override void GetDamage(int value)
    {
        Debug.Log(turretData.HP);
        if(turretData.HP - value <= 0)
        {
            turretData.HP -= value;
            Die();
            return;
        }
        turretData.HP -= value;
    }

    public override void DisplayDamage(int value)
    {
        UIDamagePooling.damagePulling.DisplayDamage(transform.position, value, Color.green, 14);
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

}
