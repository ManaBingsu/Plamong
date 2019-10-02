using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamagePooling : MonoBehaviour
{
    [SerializeField]
    public static UIDamagePooling damagePulling;

    public List<UIDamage> damageList;
    public int index;
    public int maxIndex;

    private void Awake()
    {
        if (damagePulling == null)
            damagePulling = this;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            damageList.Add(transform.GetChild(i).GetComponent<UIDamage>());
        }
        maxIndex = damageList.Count - 1;
    }

    public void DisplayDamage(Vector3 targetTransform, int value, Color color, int size)
    {
        if (index <= maxIndex)
        {
            damageList[index].gameObject.SetActive(true);
            // 만약 코루틴이 실행중일겨우
            if (damageList[index].cortn != null)
                StopCoroutine(damageList[index].cortn);

            damageList[index].cortn = StartCoroutine(damageList[index].DisplayDamage(targetTransform, value, color, size));
            index++;
        }
        else
        {
            index = 0;
            damageList[index].gameObject.SetActive(true);
            // 만약 코루틴이 실행중일겨우
            if (damageList[index].cortn != null)
                StopCoroutine(damageList[index].cortn);

            damageList[index].cortn = StartCoroutine(damageList[index].DisplayDamage(targetTransform, value, color, size));
            index++;
        }
    }
}
