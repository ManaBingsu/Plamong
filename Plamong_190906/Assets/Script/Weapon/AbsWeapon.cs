using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData weaponData;

    public abstract IEnumerator MouseAttack1(Vector3 targetPos);
    /*
    public abstract IEnumerator SkillQ();
    public abstract IEnumerator SkillE();
    */
}
