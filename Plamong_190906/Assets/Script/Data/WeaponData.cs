using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stat")]
    [SerializeField]
    private int weaponDamage;
    public int WeaponDamage
    {
        get { return weaponDamage; }
        set
        {
            weaponDamage = value;
        }
    }
}
