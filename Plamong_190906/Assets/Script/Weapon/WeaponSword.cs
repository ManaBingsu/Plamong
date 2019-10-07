﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : AbsWeapon
{
    private PlayerController player;
    [Header("Value")]
    [SerializeField]
    private float swingRange;
    [SerializeField]
    private float swingSpeed;
    [SerializeField]
    private int swingDirection;
    [Header("Reference")]
    [SerializeField]
    private Transform rangeTransform;
    //Sword Sprite
    [SerializeField]
    private Transform swordTransform;
    [SerializeField]
    private Vector3 targetY;
    [SerializeField]
    private WeaponRangeObj rangeObj;

    // 공격중임
    [SerializeField]
    private bool IsAttack;

    private void Start()
    {
        player = PlayerController.player;
        rangeObj.gameObject.SetActive(false);
        swingDirection = 1;
    }

    public override IEnumerator MouseAttack1(int damage, Transform attackerTransform, Vector3 targetPos)
    {
        if (isDelay)
            yield break;

        rangeObj.damage = damage;
        rangeObj.attackerTransform = attackerTransform;
        player.isActing = true;
        yield return StartCoroutine(DashToAttackDirection(targetPos));
        player.isActing = false;
        yield return null;
    }

    public IEnumerator DashToAttackDirection(Vector3 targetPos)
    {
        isDelay = true;
        StartCoroutine(DelayTimer());
        Vector3 direction = (targetPos - player.transform.position).normalized;
        float dashTime = 0.05f;
        float rushVelocity = 12f;
        float time = 0f;
        
        // 휘두르기 직전의 경직
        yield return new WaitForSeconds(0.1f);

        // 휘두르는 코루틴
        StartCoroutine(SwingSword(targetPos));
        // 앞대쉬
        while(time <= dashTime)
        {
            time += Time.deltaTime;
            player.transform.Translate(direction * rushVelocity * Time.deltaTime);
            yield return null;
        }

        // 휘두른 뒤의 경직
        yield return new WaitForSeconds(0.1f);
    }
    
    IEnumerator SwingSword(Vector3 targetPos)
    {
        // 무기 휘두르는 방향 변경
        swingDirection *= -1;

        rangeObj.gameObject.SetActive(true);
        Vector3 targetY = (targetPos - player.transform.position).normalized * 360f;
        rangeTransform.rotation = Quaternion.LookRotation(targetY);
        // origin rotation value
        Vector3 originRot = rangeTransform.rotation.eulerAngles;
        //최소, 최대 회전값
        
        float firstRotation = originRot.y + swingRange * (swingDirection);
        float lastRotation = originRot.y - swingRange * (swingDirection);
        // origin sprite value
        Vector3 originSprRot = swordTransform.rotation.eulerAngles;
        float fisrtSprRot = originSprRot.z + swingRange * (swingDirection);
        float lastSprRot = originSprRot.z - swingRange * (swingDirection);

        float time = 0f;
        // 무기 애니메이션 재생
        if(swingDirection == 1)
            weaponAnimator.SetTrigger("trgAttack");
        else
            weaponAnimator.SetTrigger("trgAttackLeft");

        while (time <= 1f)
        {
            time += Time.deltaTime * swingSpeed;
            float rot = Mathf.Lerp(firstRotation, lastRotation, time);
            float sprRot = Mathf.Lerp(fisrtSprRot, lastSprRot, time);
            rangeTransform.rotation = Quaternion.Euler(new Vector3(0f, player.transform.rotation.y + rot, 0f));
            swordTransform.rotation = Quaternion.Euler(new Vector3(45f, 0f , player.transform.rotation.z + sprRot));
            yield return null;
        }
        rangeObj.gameObject.SetActive(false);
        swordTransform.rotation = Quaternion.Euler(originSprRot);
        yield return null;
    }
}
