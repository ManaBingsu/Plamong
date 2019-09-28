using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Skill/TurretData")]
public class TurretData : ScriptableObject
{
    [SerializeField]
    protected int hp;
    public int HP
    {
        get { return hp; }
        set
        {
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
