using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Skill/TurretData")]
public class TurretData : ScriptableObject
{
    public delegate void DmgEventHandler(int a);

    [SerializeField]
    protected int hp;
    public event DmgEventHandler EvHP;
    public int HP
    {
        get { return hp; }
        set
        {
            EvHP?.Invoke(hp - value);
            hp = value;
        }
    }

    [SerializeField]
    protected int titanium;
    public int Titanium
    {
        get { return titanium; }
        set
        {
            titanium = value;
        }
    }

    [SerializeField]
    protected int damage;
    public int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

    [SerializeField]
    protected float attackDelay;
    public float AttackDelay
    {
        get { return attackDelay; }
        set
        {
            attackDelay = value;
        }
    }

    [SerializeField]
    protected float decompTime;
    public float DecompTime
    {
        get { return decompTime; }
        set
        {
            decompTime = value;
        }
    }

    [SerializeField]
    protected float interactDistance;
    public float InteractDistance
    {
        get { return interactDistance; }
        set
        {
            interactDistance = value;
        }
    }
}
