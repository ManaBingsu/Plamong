using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbsEnemy : MonoBehaviour
{
    public enum CrowdControl { Idle, Slow, Stun, KnockBack }
    public CrowdControl ccState;
    // 몬스터의 기본적 데이터
    [SerializeField]
    protected EnemyData enemyData;
    // 애니메이터
    [SerializeField]
    protected Animator animator;
    // 몬스터 어그로 표시
    [SerializeField]
    protected GameObject stateMark;
    // 네비게이션 컴포넌트
    [SerializeField]
    protected NavMeshAgent nav;
    // 스프라이트가 붙어있는 옵젝 트랜스폼
    [SerializeField]
    protected Transform spriteTransform;
    // 스프라이트 옵젝이 유지해야하는 회전값
    [SerializeField]
    protected Vector3 originRotation;

    // 추적 대상
    [SerializeField]
    protected Transform target;
    // 대상과의 거리
    [SerializeField]
    protected float targetDistance;
    // 공격 범위
    [SerializeField]
    protected float attackDistance;

    // 구현해줘야 하는 것들-------------------------------

    // 행동을 관리해주는 함수
    public abstract void ManageState();
    // 행동에 따라 함수를 실행
    public abstract void CallFunctionByState();
    // 상태에 따른 행동들
    public abstract IEnumerator Attack();
    public abstract void Move(Vector3 targetPos);
    public abstract void GetDamage(int value);
    public abstract void Die();
    // CC기 걸림
    public abstract void GetCrowdControl(Transform attacker, CrowdControl cc, float ccTime);
    // 어그로 표시 함수
    public abstract IEnumerator DisplayMark();

    // 구현 안하고 유용하게 사용만 하면 되는 것들 ----------------------
    // 타겟과의 거리 측정
    public float GetTargetDistance()
    {
        Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);

        float distance = Vector3.Distance(myPos, targetPos);
        return distance;
    }
    public void SetSpriteRotation()
    {
        spriteTransform.rotation = Quaternion.Euler(originRotation);
    }
}
