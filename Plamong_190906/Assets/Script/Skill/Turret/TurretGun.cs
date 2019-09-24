using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : AbsTurret
{
    // 데미지
    [SerializeField]
    private int bulletDamage;
    // 공격이 나가는 곳
    [SerializeField]
    private Transform gun;
    // 총알 공급 클래스
    [SerializeField]
    private BulletPulling bulletPulling;
    // 공격 대상
    [SerializeField]
    private AbsEnemy target;
    // 공격 대상 고유 번호
    [SerializeField]
    private int targetID;
    public AbsEnemy Target
    {
        get { return target; }
        set
        {
            if (value != null)
            {
                target = value;
                targetID = target.gameObject.GetInstanceID();
                coAttack = StartCoroutine(Attack());
            }
            else
            {
                if (coAttack != null)
                    StopCoroutine(coAttack);
                target = null;
                targetID = 0;
            }
        }
    }
    // 공격 담당 코루틴, 정지해야댐
    [SerializeField]
    private Coroutine coAttack;
    // 공격 딜레이
    [SerializeField]
    private float attackDelay;
    // 딜레이 시간
    [SerializeField]
    private WaitForSeconds DelayTime;
    // 터렛 총알 속도
    [SerializeField]
    private float bulletVelociy;

    private void Start()
    {
        DelayTime = new WaitForSeconds(attackDelay);
        // 적의 트리거 콜라이더 무시
        
    }


    // 적 인식
    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Enemy" && col.isTrigger == false)
        {
            if (Target == null)
            {
                Target = col.GetComponent<AbsEnemy>();
            }

        }
    }
    // 적 대상 변경
    private void OnTriggerExit(Collider col)
    {

        if (col.tag == "Enemy" && col.isTrigger == false && col.gameObject.GetInstanceID() == targetID)
        {
            Target = null;
        }
    }

    IEnumerator Attack()
    {
        while(true)
        {
            Vector3 targetPos = Target.transform.position;
            Vector3 firstPos = transform.position;
            firstPos.y = 0.5f;
            targetPos.y = 0.5f;
            bulletPulling.ShotBullet(bulletDamage, firstPos, targetPos, bulletVelociy, BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Trace);
            yield return DelayTime;
        }
    }
}
