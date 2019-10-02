using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataStore : MonoBehaviour
{
    public static SkillDataStore skillDataStore;

    public List<SkillData> skillDataList;

    private void Awake()
    {
        if (skillDataStore == null)
            skillDataStore = this;
    }
}
