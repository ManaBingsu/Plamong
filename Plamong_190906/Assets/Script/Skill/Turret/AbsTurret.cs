using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTurret : MonoBehaviour
{
    public enum State { Idle, Attack, Die }
    public State state;


    public SkillData skillData;

    [SerializeField]
    public TurretData turretData;
    
    // 회전해야 할 총
    [SerializeField]
    protected Transform gunTransform;
    // 공격 대상
    [SerializeField]
    protected Transform target;
    // 분해 중
    [SerializeField]
    protected bool isDecomp;
    // 상호작용 거리
    

    // 분해 중인 코루틴
    [SerializeField]
    public Coroutine cortnDecomp;

    private void Awake()
    {
        //skillData = Instantiate(skillData) as SkillData;
    }

    protected virtual void Start()
    {
        //skillData = Instantiate(skillData) as SkillData;
        StartCoroutine(ConsumeTitanium());
    }

    public IEnumerator Decomposition()
    {
        float time = 0f;
        while(time < turretData.DecompTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        PlayerController.player.playerData.Titanium += turretData.Titanium;
        Destroy(this.gameObject);
    }
    // 코스트 소비
    public IEnumerator ConsumeTitanium()
    {
        yield return null;
        yield return null;
        PlayerController.player.playerData.Titanium -= turretData.Titanium;
    }
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
