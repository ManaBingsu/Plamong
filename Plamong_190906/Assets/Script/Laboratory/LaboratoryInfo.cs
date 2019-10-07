using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryInfo : MonoBehaviour
{
    // 싱글톤
    public static LaboratoryInfo laboratory;
    // 연구실 데이터
    public LaboratoryData laboratoryData;

    private void Awake()
    {
        if (laboratory == null)
            laboratory = this;
    }

    private void Start()
    {
        // 연구실 데이터 로드
        laboratoryData = Instantiate(this.laboratoryData) as LaboratoryData;
    }


}
