using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyJellyfish : MonoBehaviour
{
    /*
    [SerializeField]
    private UIDamagePulling damagePulling;

    public enum State { Move, Attack }
    public State state;

    [SerializeField]
    private Coroutine CoAttack;

    private void Start()
    {
        // 적 데이터 복제
        LoadEnemyData();
        // 죽음 이벤트 추가
        enemyData.EvDie += new EnemyData.EventHandler(Die);
        // 데미지 풀링 참조
        damagePulling = UIDamagePulling.damagePulling;
        // 연구실로 ㄱㄱ
        target = LaboratoryInfo.laboratory.transform;
        originRotation = spriteTransform.rotation.eulerAngles;
    }
    private void Update()
    {
        ManageState();
        CallFunctionByState();
    }

    private void LateUpdate()
    {
        // 스프라이트 회전값 고정, 모든 몬스터 필수
        SetSpriteRotation();
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player" && ccState == CrowdControl.Idle)
        {
            target = col.transform;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = LaboratoryInfo.laboratory.transform;
            nav.ResetPath();
        }
    }

    public void SetTarget(Transform value)
    {

    }

    // 행동을 관리해주는 함수
    public override void ManageState()
    {
        if (target)
        {
            targetDistance = GetTargetDistance();
            if (targetDistance < attackDistance)
            {
                state = State.Attack;
            }
            else
            {
                state = State.Move;
            }
        }
        else
        {
            state = State.Move;
            target = LaboratoryInfo.laboratory.transform;
        }
    }
    // 행동에 따라 함수를 실행
    public override void CallFunctionByState()
    {
        switch(state)
        {
            case State.Move:
                Move(target.position);
                break;
            case State.Attack:
                CoAttack = StartCoroutine(Attack());
                break;
        }
    }
    // 상태에 따른 행동들
    // 공격
    public override IEnumerator Attack()
    {
        nav.isStopped = true;
        yield return new WaitForSeconds(2.0f);
        nav.isStopped = false;
    }
    // 이동
    public override void Move(Vector3 targetPos)
    {
        nav.SetDestination(targetPos);
    }
    // 데미지 받음
    public override void GetDamage(int value)
    {
        // 데미지 주기
        enemyData.HP -= value;
        // 데미지 표기
        damagePulling.DisplayDamage(transform.position, value);
    }
    // CC기 받음
    public override void GetCrowdControl(Transform attacker, CrowdControl cc, float ccTime)
    {
        nav.SetDestination(attacker.position);

        if(cc == CrowdControl.KnockBack)
        {
            if (ccState == CrowdControl.KnockBack)
                return;
        }

        ccState = cc;

        switch (ccState)
        {
            case CrowdControl.Idle:
                break;
            case CrowdControl.KnockBack:
                    StartCoroutine(KnockBack(attacker, ccTime));
                break;
            case CrowdControl.Slow:
                break;
            case CrowdControl.Stun:
                break;
        }
    }
    // 죽음
    public override void Die()
    {
        Debug.Log("Die");
    }
    // 어그로 표시 함수
    public override IEnumerator DisplayMark() { yield return null; }
    // CC기
    // 넉백
    public IEnumerator KnockBack(Transform attacker, float ccTime)
    {
        // 넉백 물리 효과 적용을 위해 무조건 초기화 해주어야 함
        nav.isStopped = true;
        nav.ResetPath();

        Vector3 targetPos = new Vector3(attacker.position.x, 0f, attacker.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);
        float backSpeed = 1.5f;

        float time = 0f;
        while (time <= ccTime)
        {
            time += Time.deltaTime;
            nav.velocity = (myPos - targetPos).normalized * backSpeed;
            yield return null;
        }
        ccState = CrowdControl.Idle;
        nav.isStopped = false;
    }
    */
}
