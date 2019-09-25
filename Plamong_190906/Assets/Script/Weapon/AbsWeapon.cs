using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData weaponData;
    // 무기 고유 딜레이
    public float delay;
    public bool isDelay;

    public abstract IEnumerator MouseAttack1(int damage, Transform attacker, Vector3 targetPos);
    /*
    public abstract IEnumerator SkillQ();
    public abstract IEnumerator SkillE();
    */

    public virtual IEnumerator DelayTimer()
    {
        float time = 0f;
        while (time <= delay)
        {
            time += Time.deltaTime;
            yield return null;
        }
        isDelay = false;
    }
}
