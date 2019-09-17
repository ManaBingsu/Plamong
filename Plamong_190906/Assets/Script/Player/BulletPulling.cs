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
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            bulletList.Add(transform.GetChild(i).GetComponent<BulletInfo>());
        }
        maxIndex = bulletList.Count;
    }

    public void ShotBullet(Vector3 position, float velocity, BulletInfo.ShotType shotT, BulletInfo.SpriteType sprT)
    {
        if(index < maxIndex)
        {
            bulletList[index].gameObject.SetActive(true);
            bulletList[index++].Shot(position, velocity, shotT, sprT);
        }
        else
        {
            index = 0;
            bulletList[index].gameObject.SetActive(true);
            bulletList[index++].Shot(position, velocity, shotT, sprT);
        }
    }
}
