﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    // 함수 대리자, UI 이벤트
    public delegate void EventHandler();
    // 함수 대리자, 데미지 이벤트
    public delegate void EventValueHandler(int x);

    // 스탯
    [Header("Stat")]
    [SerializeField]
    private int maxDurability;
    public event EventHandler EvMaxDurability;
    public int MaxDurability
    {
        get { return maxDurability; }
        set
        {
            maxDurability = value;
            EvDurability();
        }
    }
    [SerializeField]
    private int durability;
    public event EventHandler EvDurability;
    public event EventValueHandler EvValueDurability;
    public int Durability
    {
        get { return durability; }
        set
        {
            if (value < 1)
            {
                EvValueDurability(durability - value);
                durability = 0;
                //EvDie();
            }

            EvValueDurability(durability - value);
            durability = value;
            EvDurability();
        }
    }

    [SerializeField]
    private int maxPower;
    public event EventHandler EvMaxPower;
    public int MaxPower
    {
        get { return maxPower; }
        set
        {
            maxPower = value;
        }
    }
    [SerializeField]
    private int power;
    public event EventHandler EvPower;
    public int Power
    {
        get { return power; }
        set
        {
            power = value;
            EvPower();
        }
    }

    [SerializeField]
    private int maxTitanium;
    public event EventHandler EvMaxTitanium;
    public int MaxTitanium
    {
        get { return maxTitanium; }
        set
        {
            maxTitanium = value;
        }
    }
    [SerializeField]
    private int titanium;
    public event EventHandler EvTitanium;
    public int Titanium
    {
        get { return titanium; }
        set
        {
            titanium = value;
            EvTitanium();
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

    [SerializeField]
    private int skillPoint;
    public event EventHandler EvSkillPoint;
    public int SkillPoint
    {
        get { return skillPoint; }
        set
        {
            skillPoint = value;
            EvSkillPoint();
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

    [SerializeField]
    private float transformSpeed;
    public float TransformSpeed
    {
        get { return transformSpeed; }
        set
        {
            transformSpeed = value;
        }
    }
}
