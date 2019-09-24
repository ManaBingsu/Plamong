using System.Collections;
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
    [Header("Reference")]
    [SerializeField]
    private Transform rangeTransform;
    [SerializeField]
    private Vector3 targetY;

    // 공격중임
    [SerializeField]
    private bool IsAttack;

    private void Start()
    {
        player = PlayerController.player;
    }

    private void Update()
    {

    }

    public override IEnumerator MouseAttack1(int damage, Vector3 targetPos)
    {
        player.IsActing = true;
        yield return StartCoroutine(DashToAttackDirection(targetPos));
        player.IsActing = false;
        yield return null;
    }

    public IEnumerator DashToAttackDirection(Vector3 targetPos)
    {
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
        Vector3 targetY = (targetPos - player.transform.position).normalized * 360f;
        rangeTransform.rotation = Quaternion.LookRotation(targetY);
        // origin rotation value
        Vector3 originRot = rangeTransform.rotation.eulerAngles;
        //최소, 최대 회전값
        float firstRotation = originRot.y + swingRange;
        float lastRotation = originRot.y - swingRange;

        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime * swingSpeed;
            float rot = Mathf.Lerp(firstRotation, lastRotation, time);
            rangeTransform.rotation = Quaternion.Euler(new Vector3(0f, player.transform.rotation.y + rot, 0f));
            yield return null;
        }

        yield return null;
    }
}
