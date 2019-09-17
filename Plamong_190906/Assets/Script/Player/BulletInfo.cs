using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    // 총알이 나가는 목표점
    public Vector3 targetPos;
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

    public void Shot(Vector3 position, float velocity, ShotType shotT, SpriteType sprT)
    {
        targetPos = position;
        moveVelocity = velocity;
        shotType = shotT;
        sprT = spriteType;
        transform.position = PlayerController.player.transform.position;

        StartCoroutine(TranslateBullet());
    }

    public IEnumerator TranslateBullet()
    {
        // 타겟의 방향 지정
        Vector3 dir = (targetPos - transform.position).normalized;
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
