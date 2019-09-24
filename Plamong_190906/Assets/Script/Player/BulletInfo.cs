using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    // 데미지
    public int bulletDamage;
    // 총알이 나가는 목표점
    public Vector3 targetPosition;
    // 총알 속도
    public float moveVelocity;
    // 총알이 날아가는 시간
    public float existTime;
    // 총알 유형, 스프라이트 유형
    public enum ShotType { Straight, Trace }
    public enum SpriteType { Straight, Trace }

    public ShotType shotType;
    public SpriteType spriteType;

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
            enemy.GetCrowdControl(this.transform, AbsEnemy.CrowdControl.KnockBack, 0.1f);
            enemy.GetDamage(bulletDamage);
            gameObject.SetActive(false);
        }
    }

    public void Shot(int damage, Vector3 firstPos, Vector3 targetPos, float velocity, ShotType shotT, SpriteType sprT)
    {
        targetPosition = targetPos;
        moveVelocity = velocity;
        shotType = shotT;
        sprT = spriteType;
        transform.position = firstPos;
        bulletDamage = damage;

        StartCoroutine(TranslateBullet());
    }

    public IEnumerator TranslateBullet()
    {
        // 타겟의 방향 지정, y 값이 동일해야한다.
        targetPosition.y = 0.5f;
        Vector3 myPos = transform.position;
        myPos.y = 0.5f;
        Vector3 dir = (targetPosition - myPos).normalized;
        // 정해진 범위만큼 날아가기
        float time = 0f;
        while(time < existTime)
        {
            transform.Translate(dir * moveVelocity * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
