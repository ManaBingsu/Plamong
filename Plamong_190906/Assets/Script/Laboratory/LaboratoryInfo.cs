using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryInfo : MonoBehaviour
{
    public static LaboratoryInfo laboratory;

    private void Awake()
    {
        if (laboratory == null)
            laboratory = this;
    }
}
