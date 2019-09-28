using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 스테이지가 끝난 후 쉬는 시간, 아직 구현 안됨
    public bool isRest;
    [SerializeField]
    private float restTime;
    public float RestTime
    {
        get { return restTime; }
        set
        {
            restTime = value;
        }
    }

    [SerializeField]
    private AllMonsterList allMonsterList;

    [Header("Direction Transform")]
    public List<Transform> directionList;

    [Header("Stage Data")]
    public List<StageData> stageList;

    private void Start()
    {
        StartCoroutine(StageDispencer());
    }

    IEnumerator StageDispencer()
    {
        int n = 1;
        foreach(StageData stg in stageList)
        {
            // 스테이지 실행
            Debug.Log(n + " 번째 스테이지 시작");
            yield return StartCoroutine(WaveDispencer(stg));
            // 스테이지 몬스터가 정리될 때까지 정지
            /*while (isRest == true)
                yield return null;*/
            // 스테이지 끝난 후 쉬는 시간
            yield return new WaitForSeconds(stg.stageRestTime);
        }

        Debug.Log("모든 스테이지 끝!!!!!!!!!!!!!");
    }

    public IEnumerator WaveDispencer(StageData stg)
    {
        int n = 1;
        foreach (WaveData wvData in stg.waveDataList)
        {
            // 웨이브 실행
            Debug.Log(n + " 번째 웨이브 시작");
            yield return StartCoroutine(LevelDispencer(wvData.levelList));
            // 웨이브 끝난 후 쉬는 시간
            Debug.Log(n++ + " 번째 웨이브 쉬는 시간");
            yield return new WaitForSeconds(wvData.waveRestTime);
        }
    }
    // 미완성 스폰 코드
    IEnumerator LevelDispencer(List<Level> lvList)
    {
        foreach (Level lv in lvList)
        {
            // 몬스터 소환 약간 씩 다른 오차로
            for(int i = 0; i < lv.monsterNumber; i++)
            {
                Instantiate(allMonsterList.monsterList[0], directionList[(int)lv.spawnDirection].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(lv.nextTime);
        }
    }
}
