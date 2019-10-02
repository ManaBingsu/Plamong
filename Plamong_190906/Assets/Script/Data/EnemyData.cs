using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public delegate void EventHandler();
    public delegate void EventValueHandler(int value);

    [Header("Stat")]
    [SerializeField]
    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
        }
    }

    [SerializeField]
    private int hP;
    public event EventHandler EvHP;
    public event EventHandler EvDie;
    public event EventValueHandler EvValueHP;
    public int HP
    {
        get { return hP; }
        set
        {
            if (value < 1)
            {
                EvValueHP(hP - value);
                hP = 0;
                EvDie();
            }
            else
            {
                EvValueHP(hP - value);
                hP = value;
                //EvHP();
            }
        }
    }

    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

    [Header("Speed stat")]
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
        }
    }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
        }
    }

    [SerializeField]
    private float castingSpeed;
    public float CastingSpeed
    {
        get { return castingSpeed; }
        set
        {
            castingSpeed = value;
        }
    }
}
