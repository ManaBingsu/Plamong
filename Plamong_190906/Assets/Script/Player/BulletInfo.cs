using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    // 시전자
    public Transform attackerTransform;
    // 데미지
    public int bulletDamage;
    // 총알이 나가는 목표점
    public Vector3 targetPosition;
    // 총알 속도
    public float moveVelocity;
    // 총알 발사 위치
    public float shotPosValue;
    // 총알이 날아가는 시간
    public float existTime;
    // 총알 유형, 스프라이트 유형
    public enum ShotType { Straight, Trace }
    public enum SpriteType { Straight, Trace }

    public ShotType shotType;
    public SpriteType spriteType;

    // cc기 관련
    public AbsEnemy.CrowdControl ccState;
    public float ccTime;
    public float ccPower;

    private void Start()
    {
        // 임시
        existTime = 1.0f;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy" && col.isTrigger == false)
        {
            AbsEnemy enemy = col.gameObject.GetComponent<AbsEnemy>();
            enemy.GetCrowdControl(ccState, attackerTransform, ccTime, ccPower);
            enemy.GetDamage(bulletDamage, attackerTransform);
            gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Laboratory" && col.isTrigger == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void Shot(int damage, Transform attacker, Vector3 targetPos, float velocity, ShotType shotT, SpriteType sprT)
    {
        attackerTransform = attacker;

        targetPosition = targetPos;
        moveVelocity = velocity;
        shotType = shotT;
        sprT = spriteType;
        transform.position = attacker.position;
        bulletDamage = damage;
        ccState = AbsEnemy.CrowdControl.None;
        ccTime = 0f;
        ccPower = 0f;

        StartCoroutine(TranslateBullet());
    }
    public void Shot(int damage, Transform attacker, Vector3 targetPos, float velocity, BulletInfo.ShotType shotT, BulletInfo.SpriteType sprT, AbsEnemy.CrowdControl cc, float cTime, float power)
    {
        attackerTransform = attacker;

        targetPosition = targetPos;
        moveVelocity = velocity;
        shotType = shotT;
        sprT = spriteType;
        transform.position = attacker.position;
        bulletDamage = damage;
        ccState = cc;
        ccTime = cTime;
        ccPower = power;

        StartCoroutine(TranslateBullet());
    }

    public IEnumerator TranslateBullet()
    {
        // 타겟의 방향 지정, y 값이 동일해야한다.
        targetPosition.y = 0.5f;
        Vector3 myPos = transform.position;
        myPos.y = 0.5f;

        Vector3 dir = (targetPosition - myPos).normalized;
        // 몸 앞에서 나가기
        transform.Translate(dir * shotPosValue);
        // 정해진 범위만큼 날아가기
        float time = 0f;
        while(time < existTime)
        {
            transform.Translate(dir * moveVelocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0.65f, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
