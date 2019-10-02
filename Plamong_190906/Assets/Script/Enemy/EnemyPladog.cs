using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPladog : AbsEnemy
{
    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Start()
    {
        base.Start();
        enemyData.EvDie += new EnemyData.EventHandler(Die);
        enemyData.EvValueHP += new EnemyData.EventValueHandler(DisplayDamage);
    }
    protected override void Update()
    {
        base.Update();
        ManageTarget();
        ManageState();
        CallFunctionByState();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();

    }

    private void OnTriggerStay(Collider col)
    {
        // 연구실 공격 중엔 어그로 안끌림
        if (isAttackLaboratory)
            return;
        // 어그로 끌린 중엔 타겟 불가
        if (isAggro)
            return;
        // 이미 한 명을 추적하고 있는 경우 타겟 불가
        
        if (target !=null && target.CompareTag("Player"))
            return;

        if(col.CompareTag("Player"))
        {
            target = col.transform;
        }
    }

    public override void ManageTarget()
    {
        // 연구실 공격하는 적은 어그로 안끌림
        if(isAttackLaboratory)
        {
            target = LaboratoryInfo.laboratory.transform;
        }
        // 타겟이 없는 경우 연구실로
        else if (target == null)
        {
            target = LaboratoryInfo.laboratory.transform;
            nav.destination = target.position;
        }
        // 타겟이 사망할 경우
        else if (target.gameObject.activeSelf == false)
        {
            target = null;
        }
        else
        {
            return;
        }
    }

    public override IEnumerator AggroTimer()
    {
        while(true)
        {
            if (!isAggro)
            {
                aggroTimer = 0f;
                yield return null;
                continue;
            }

            aggroTimer += Time.deltaTime;
            if(aggroTimer > aggroLimitTime)
            {
                isAggro = false;
                aggroTimer = 0f;
                target = null;
            }
            yield return null;
        }
    }

    public override void ManageState()
    {
        // 기절 시
        if (stunCounter > 0)
        {
            state = State.Idle;
            return;
        }

        if (target)
        {
            targetDistance = GetTargetDistance(target);
            // 연구실인가 아닌가에 따른 사거리 차이
            float compare = target.CompareTag("Laboratory") ? laborAttackDistance : attackDistance;

            if (targetDistance < compare)
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
        }
    }

    public override void CallFunctionByState()
    {
        switch (state)
        {
            case State.Idle:
                //nav.isStopped = true;
                break;
            case State.Move:
                //nav.isStopped = false;
                Move();
                break;
            case State.Attack:
                if (isAttack)
                    break;
                StartCoroutine(Attack());
                break;
        }
    }

    public override void Move()
    {
        nav.destination = target.position;
    }

    public override IEnumerator Attack()
    {
        isAttack = true;
        nav.isStopped = true;
        // 공격 모션
        yield return new WaitForSeconds(0.3f);
        // 공격 모션 후에도 그 자리에 있으면 데미지
        if(attackDistance > GetTargetDistance(target))
        {
            PlayerController.player.playerData.Durability -= enemyData.Damage;
        }
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        nav.isStopped = false;
        yield return null;
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void GetDamage(int value, Transform attacker)
    {
        enemyData.HP -= value;
        // 어그로 타이머 초기화

        // 처음으로 어그로를 끌린다면 맨 처음 대상 락온
        if (!isAggro)
        {
            isAggro = true;
            aggroTimer = 0f;
            target = attacker;
        }
        else
        {
            aggroTimer = 0f;
            if (GetTargetDistance(attacker) < GetTargetDistance(target))
            {
                target = attacker;
            }
        }
    }

    public override void DisplayDamage(int value)
    {
        UIDamagePooling.damagePulling.DisplayDamage(transform.position, value, dmgColor, 14);
    }

    public override void GetCrowdControl(CrowdControl cc, Transform attacker, float ccTime, float power)
    {
        switch(cc)
        {
            case CrowdControl.KnockBack:
                //이전 넉백 취소하기
                if (knockBackCoroutine != null)
                    StopCoroutine(knockBackCoroutine);
                knockBackCoroutine = StartCoroutine(KnockBack(attacker, ccTime, power));
                break;
            case CrowdControl.Slow:
                StartCoroutine(Slow(ccTime, power));      
                break;
            case CrowdControl.Stun:
                StartCoroutine(Stun(ccTime));
                break;
        }
    }

    public override IEnumerator KnockBack(Transform attacker, float ccTime, float power)
    {
        if (gameObject.activeSelf == false)
            yield break;

        //nav.isStopped = true;

        Vector3 firstPos = new Vector3(transform.position.x, 0f, transform.position.y);
        Vector3 targetPos = new Vector3(attacker.position.x, 0f, transform.position.y);

        Vector3 direction = (firstPos - targetPos).normalized;
        float knockBackSpeed = power;

        float time = 0f;
        while(time < ccTime)
        {
            yield return null;
            time += Time.deltaTime;
            nav.velocity = direction * knockBackSpeed * Time.deltaTime;
        }
        //nav.isStopped = false;
    }

    public override IEnumerator Slow(float ccTime, float power)
    {
        slowPower *= power;
        yield return new WaitForSeconds(ccTime);
        slowPower /= power;
    }

    public override IEnumerator Stun(float ccTime)
    {
        stunCounter++;
        yield return new WaitForSeconds(ccTime);
        stunCounter--;
    }
 
}
