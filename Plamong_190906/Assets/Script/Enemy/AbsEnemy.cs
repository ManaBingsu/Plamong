using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbsEnemy : MonoBehaviour
{
    public enum State { Idle, Move, Attack, Damaged, Die }
    public enum CrowdControl { Slow, Stun, KnockBack, None }
    [Header("Data")]
    // 몬스터의 기본적 데이터
    public EnemyData enemyData;
    [Header("State")]
    // 기본적인 상태
    public State state;
    // 걸릴 수 있는 cc기 종류\
    public CrowdControl ccState;

    [Header("Value")]
    // 공식-> moveSpeed = enemyData.MoveSpeed * slowPower * Time.deltaTime;
    protected float moveSpeed;
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
    // 연구실을 공격하고 있는가
    [SerializeField]
    protected bool isAttackLaboratory;
    // 어그로 끌렸는가
    [SerializeField]
    protected bool isAggro;
    // 어그로 타이머
    [SerializeField]
    protected float aggroTimer;
    // 어그로 제한 시간
    [SerializeField]
    protected float aggroLimitTime;

    [Header("Crowd Control")]
    // 넉백 코루틴, 넉백 중복 시 먼저 맞은 넉백 중단
    [SerializeField]
    protected Coroutine knockBackCoroutine;
    // 이동속도에 곱해지는 둔화력
    [SerializeField]
    protected float slowPower;
    // 기절 카운터, 중복 기절 적용용 수치
    [SerializeField]
    protected int stunCounter;

    [Header("Parent Reference")]
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
    [Header("UI Reference")]
    [SerializeField]
    protected UIDamagePooling damageUI;

    protected virtual void Awake()
    {
        //부모 컴포넌트 참조
        nav = GetComponent<NavMeshAgent>();
        
        //자식 컴포넌트 참조, Sprite 가 붙어있는 자식은 항상 첫번째 자식으로 둘것
        animator = transform.GetChild(0).GetComponent<Animator>();
        spriteTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        LoadEnemyData();
        // UI Damage 참조
        damageUI = UIDamagePooling.damagePulling;

        // 회전값 고정
        originRotation = spriteTransform.rotation.eulerAngles;
        // 어그로 타이머 시작
        StartCoroutine(AggroTimer());
        // 슬로우 수치
        slowPower = 1f;
        // 이동 속도
    }

    protected virtual void Update()
    {
        moveSpeed = enemyData.MoveSpeed * slowPower * Time.deltaTime;
        nav.speed = moveSpeed;
    }

    protected virtual void LateUpdate()
    {
        spriteTransform.rotation = Quaternion.Euler(originRotation);
    }

    // 구현해줘야 하는 것들-------------------------------

    // 타겟을 관리해주는 함수(ex: 아무도 없으면 연구실)
    public abstract void ManageTarget();
    // 어그로 타이머, 항상 돌고 있다
    public abstract IEnumerator AggroTimer();
    // 행동을 관리해주는 함수
    public abstract void ManageState();
    // 행동에 따라 함수를 실행
    public abstract void CallFunctionByState();
    // 공격
    public abstract IEnumerator Attack();
    // 이동
    public abstract void Move(); 
    // 죽음
    public abstract void Die();
    // CC기 걸림
    public abstract void GetCrowdControl(CrowdControl cc, Transform attacker, float ccTime, float power);

    // Virtual 인 함수들, 노력해보자

    // 데미지를 받음
    public abstract void GetDamage(int value, Transform attacker);
    // 받은 데미지 표시
    public abstract void DisplayDamage(int value, Color color, float size);
    // CC : 넉백
    public abstract IEnumerator KnockBack(Transform attacker, float ccTime, float power);
    // CC : 둔화
    public abstract IEnumerator Slow(float ccTime, float power);
    // CC : 기절
    public abstract IEnumerator Stun(float ccTime);

    // 구현 안하고 사용만 하면 되는 것들 ----------------------
    // 타겟과의 거리 측정
    public float GetTargetDistance(Transform tg)
    {
        Vector3 targetPos = new Vector3(tg.position.x, 0f, tg.position.z);
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
