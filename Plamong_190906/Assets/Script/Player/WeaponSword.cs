using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : AbsWeapon
{
    private PlayerController player;

    private void Start()
    {
        player = PlayerController.player;
    }

    public override IEnumerator MouseAttack1(Vector3 targetPos)
    {
        player.IsActing = true;
        yield return StartCoroutine(DashToAttackDirection(targetPos));
        player.IsActing = false;
        yield return null;
    }

    public IEnumerator DashToAttackDirection(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - player.transform.position).normalized;
        float dashTime = 0.05f;
        float rushVelocity = 12f;
        float time = 0f;
        // 휘두르기 직전의 경직
        yield return new WaitForSeconds(0.1f);
        while(time <= dashTime)
        {
            time += Time.deltaTime;
            player.transform.Translate(direction * rushVelocity * Time.deltaTime);
            yield return null;
        }
        // 휘두른 뒤의 경직
        yield return new WaitForSeconds(0.1f);
    }
}
