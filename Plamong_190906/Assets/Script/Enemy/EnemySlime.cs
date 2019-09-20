using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : AbsEnemy
{
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        stateMark = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        ManageState();
    }
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(DisplayMark());
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = col.transform;
        }

    }
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    // 구현 해주어야 하는 것
    public override void ManageState()
    {
        if(target)
        {
            targetDistance = Vector3.Distance(transform.position, target.position);
            if(targetDistance > attackDistance)
            {
                if (IsAttack)
                    return;

                Move();
            }
            else
            {
                if(!IsAttack)
                {
                    IsAttack = true;
                    StartCoroutine(Attack());
                }
            }

        }
        else
        {
            // 기지로 향한다.
        }
    }

    public override IEnumerator Attack()
    {
        yield return null;
        ChangeX();
        animator.SetTrigger("Attack");

        int playerDura = PlayerController.player.playerData.Durability;
        PlayerController.player.playerData.Durability = playerDura - 10;

        float time = 0f;
        while(time <= enemyData.AttackSpeed)
        {
            time += Time.deltaTime;
            yield return null;
        }

        IsAttack = false;
    }
    public override void Move()
    {
        if(target != null)
        {
            ChangeX();
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * enemyData.MoveSpeed * Time.deltaTime;
        }
    }
    public override void GetDamage()
    {

    }
    public override void Die()
    {

    }

    public void ChangeX()
    {
        if (target.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
