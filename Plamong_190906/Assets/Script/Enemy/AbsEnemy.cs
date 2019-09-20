using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsEnemy : MonoBehaviour
{
    // 몬스터의 기본적 데이터
    [SerializeField]
    protected EnemyData enemyData;
    // 애니메이터
    [SerializeField]
    protected Animator animator;
    // 스프라이트 렌더러
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    // 몬스터 어그로 표시
    [SerializeField]
    protected GameObject stateMark;

    // 추적 대상
    [SerializeField]
    protected Transform target;
    // 대상과의 거리
    [SerializeField]
    protected float targetDistance;
    // 공격 범위
    [SerializeField]
    protected float attackDistance;

    // 상태 체크
    [SerializeField]
    protected bool IsAttack;
    [SerializeField]
    protected bool IsMove;

    // 행동을 관리해주는 함수
    public virtual void ManageState() { }
    // 상태에 따른 행동들
    public virtual IEnumerator Attack() { yield return null; }
    public virtual void Move() { }
    public virtual void GetDamage() { }
    public virtual void Die() { }

    // 어그로 표시 함수
    public virtual IEnumerator DisplayMark()
    {
        yield return null;
        stateMark.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        stateMark.gameObject.SetActive(false);
    }
}
