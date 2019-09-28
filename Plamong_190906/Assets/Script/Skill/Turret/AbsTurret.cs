using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTurret : MonoBehaviour
{
    public enum State { Idle, Attack, Die }
    public State state;

    [SerializeField]
    protected TurretData turretData;
    
    // 회전해야 할 총
    [SerializeField]
    protected Transform gunTransform;
    // 공격 대상
    [SerializeField]
    protected Transform target;
    /*
    // 타겟을 관리해주는 함수
    protected abstract void ManageTarget();
    // 행동을 관리해주는 함수
    public abstract void ManageState();
    // 행동에 따라 함수를 실행
    public abstract void CallFunctionByState();
    // 공격
    protected abstract IEnumerator Attack();
    // 죽음
    public abstract void Die();*/
}
