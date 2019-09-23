using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsSkill : MonoBehaviour
{
    // 스킬 사용가능 여부
    [SerializeField]
    private bool isLearn;
    public bool IsLearn
    {
        get { return isLearn; }
        set
        {
            isLearn = value;
        }
    }
}
