using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSkill : MonoBehaviour
{
    [Header("Info")]
    public int index;
    public SkillData skillData;

    protected virtual void Start()
    {
        skillData = Instantiate(SkillDataStore.skillDataStore.skillDataList[index]) as SkillData;
    }

    public abstract void ActivateSkill();
    
}
