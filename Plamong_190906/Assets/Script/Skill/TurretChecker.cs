using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretChecker : MonoBehaviour
{
    public SkillManager skill;
    public bool isBuildPos;
    public int skillNum;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(OffMe());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && col.isTrigger == false)
        {
            isBuildPos = false;
            skill.SpawnDistance(transform.position, skillNum);
            gameObject.SetActive(false);
        }
        if (col.gameObject.CompareTag("Enemy") && col.isTrigger == false)
        {
            isBuildPos = false;
            skill.SpawnDistance(transform.position, skillNum);
            gameObject.SetActive(false);
        }
    }

    IEnumerator OffMe()
    {
        yield return null;
        yield return null;

        isBuildPos = true;
        skill.SpawnDistance(transform.position, skillNum);

        gameObject.SetActive(false);
    }
}
