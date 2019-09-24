using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbsEnemy : MonoBehaviour
{
    public enum State { Move, Attack, Damaged, Die }
    public enum CrowdControl { Idle, Slow, Stun, KnockBack }
    public enum Target { Laboratory, Player, Turret }

    [Header("State")]
    // 기본적인 상태
    public State state;
    // 걸릴 수 있는 cc기 종류\
    public CrowdControl ccState;
    // 추적 대상
    public Target targetState;

    [Header("Value")]
    // 추적 대상
    [SerializeField]
    protected Transform target;
    // 추적대상과의 거리
    [SerializeField]
    protected float targetDistance;
    // 일반 공격 거리
    [SerializeField]
    protected float attackDistance;
    // 연구실 공격 거리
    [SerializeField]
    protected float laborAttackDistance;

    [Header("Parent Reference")]
    // 몬스터의 기본적 데이터
    public EnemyData enemyData;
    // 네비게이션 컴포넌트
    [SerializeField]
    protected NavMeshAgent nav;

    [Header("Child Reference")]
    // 애니메이터
    [SerializeField]
    protected Animator animator;
    // 몬스터 어그로 표시
    [SerializeField]
    protected GameObject stateMark;
    // 스프라이트가 붙어있는 옵젝 트랜스폼
    [SerializeField]
    protected Transform spriteTransform;
    // 스프라이트 옵젝이 유지해야하는 회전값
    [SerializeField]
    protected Vector3 originRotation;

    private void Awake()
    {
        //부모 컴포넌트 참조
        nav = GetComponent<NavMeshAgent>();

        //자식 컴포넌트 참조, Sprite 가 붙어있는 자식은 항상 첫번째 자식으로 둘것
        animator = transform.GetChild(0).GetComponent<Animator>();
        spriteTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    private void Start()
    {
        // 회전값 고정
        originRotation = spriteTransform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        spriteTransform.rotation = Quaternion.Euler(originRotation);
    }

    // 구현해줘야 하는 것들-------------------------------

    // 타겟을 관리해주는 함수
    public abstract void SetTarget();
    // 행동을 관리해주는 함수
    public abstract void ManageState();
    // 행동에 따라 함수를 실행
    public abstract void CallFunctionByState();
    // 공격
    public abstract IEnumerator Attack();
    // 이동
    public abstract void Move();
    // 데미지를 받음
    public abstract void GetDamage(int value);
    public abstract void GetDamage(int value, Transform attacker);
    // 데미지를 받음
    public abstract void DisplayDamage(int value, Color color, float size);
    // 죽음
    public abstract void Die();
    // CC기 걸림
    public abstract void GetCrowdControl(Transform attacker, CrowdControl cc, float ccTime);
    // 어그로 표시 함수
    public abstract IEnumerator DisplayMark();

    // 구현 안하고 사용만 하면 되는 것들 ----------------------
    // 타겟과의 거리 측정
    public float GetTargetDistance()
    {
        Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);

        float distance = Vector3.Distance(myPos, targetPos);
        return distance;
    }
    // 타겟의 회전값 고정
    public void SetSpriteRotation()
    {
        spriteTransform.rotation = Quaternion.Euler(originRotation);
    }
    // 적 데이터 로드(중요)
    public void LoadEnemyData()
    {
        enemyData = Instantiate(this.enemyData) as EnemyData;
    }
}
