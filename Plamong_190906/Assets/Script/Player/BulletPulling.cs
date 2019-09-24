using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPulling : MonoBehaviour
{
    public List<BulletInfo> bulletList;
    public int index;
    public int maxIndex;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            bulletList.Add(transform.GetChild(i).GetComponent<BulletInfo>());
        }
        maxIndex = bulletList.Count - 1;
    }

    public void ShotBullet(int damage, Vector3 firstPos, Vector3 targetPos, float velocity, BulletInfo.ShotType shotT, BulletInfo.SpriteType sprT)
    {
        if(index <= maxIndex)
        {
            bulletList[index].gameObject.SetActive(true);
            bulletList[index].Shot(damage, firstPos, targetPos, velocity, shotT, sprT);
            index++;
        }
        else
        {
            index = 0;
            bulletList[index].gameObject.SetActive(true);
            bulletList[index].Shot(damage, firstPos, targetPos, velocity, shotT, sprT);
            index++;
        }
    }
}
