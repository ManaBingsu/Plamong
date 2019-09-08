using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    // 함수 대리자
    public delegate void EventHandler();
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
        }
    }
    [SerializeField]
    private int durability;
    public event EventHandler EvDurability;
    public int Durability
    {
        get { return durability; }
        set
        {
            durability = value;
        }
    }

    private int hp;

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

    [Header("Bonus stat")]
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
