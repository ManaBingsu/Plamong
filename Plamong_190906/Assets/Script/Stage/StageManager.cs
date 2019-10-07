using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager stageManager;

    public delegate void EventHandler();

    public event EventHandler EvWave;
    private WaveData currentWave;
    public WaveData CurrentWave
    {
        get { return currentWave; }
        set
        {
            currentWave = value;
            if(EvWave != null)
               EvWave();
        }
    }

    public event EventHandler EvStage;
    private StageData currentStage;
    public StageData CurrentStage
    {
        get { return currentStage; }
        set
        {
            currentStage = value;
            if(EvStage != null)
                EvStage();
        }
    }

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

    private void Awake()
    {
        if (stageManager == null)
            stageManager = this;
    }

    private void Start()
    {
        CurrentStage = stageList[0];
        CurrentWave = stageList[0].waveDataList[0];
        StartCoroutine(StageDispencer());
    }

    IEnumerator StageDispencer()
    {
        foreach(StageData stg in stageList)
        {
            CurrentStage = stg;
            CurrentWave = stg.waveDataList[0];
            // 스테이지 시작 전 쉬는 시간
            yield return new WaitForSeconds(stg.stageRestTime);
            // 스테이지 실행
            yield return StartCoroutine(WaveDispencer(stg));
        }
    }

    public IEnumerator WaveDispencer(StageData stg)
    {
        foreach (WaveData wvData in stg.waveDataList)
        {
            CurrentWave = wvData;
            // 웨이브 실행
            yield return StartCoroutine(LevelDispencer(wvData.levelList));
            // 웨이브 끝난 후 쉬는 시간
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
