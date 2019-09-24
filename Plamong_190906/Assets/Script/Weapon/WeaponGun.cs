using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : AbsWeapon
{
    [SerializeField]
    private BulletPulling bulletPulling;

    public override IEnumerator MouseAttack1(int damage, Vector3 targetPos)
    {
        bulletPulling.ShotBullet(damage, PlayerController.player.transform.position, targetPos, 30f, BulletInfo.ShotType.Straight, BulletInfo.SpriteType.Straight);
        yield return null;
    }
}
