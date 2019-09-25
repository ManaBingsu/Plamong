using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangeObj : MonoBehaviour
{
    // 시전자
    public Transform attackerTransform;
    public int damage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && col.isTrigger == false)
        {
            AbsEnemy enemy = col.gameObject.GetComponent<AbsEnemy>();
            enemy.GetCrowdControl(AbsEnemy.CrowdControl.KnockBack, attackerTransform, 0.2f, 400f);
            enemy.GetDamage(damage, attackerTransform);
        }
    }
}
