using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : AbsWeapon
{
    [SerializeField]
    private BulletPulling bulletPulling;

    public override IEnumerator MouseAttack1(Vector3 targetPos)
    {
        bulletPulling.ShotBullet(targetPos, 30f, BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Straight);
        yield return null;
    }
}
