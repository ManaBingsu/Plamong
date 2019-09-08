using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    public abstract void Attack();
    public abstract void Move();

    public abstract void GetDamage();

    public abstract void Die();
}
