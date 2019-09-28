using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : AbsWeapon
{


    [SerializeField]
    private BulletPooling bulletPulling;

    public override IEnumerator MouseAttack1(int damage, Transform attackerTransform, Vector3 targetPos)
    {
        if (isDelay)
            yield break;
        else
        {
            isDelay = true;
            StartCoroutine(DelayTimer());
            bulletPulling.ShotBullet(damage, PlayerController.player.transform, targetPos, 30f, BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Straight, AbsEnemy.CrowdControl.KnockBack, 1f, 0f);
            yield return null;
        }
       
    }

   
}
