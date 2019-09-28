using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawning", menuName = "Spawning/WaveData")]
public class WaveData : ScriptableObject
{
    // 웨이브 끝난 후 쉬는 시간
    public float waveRestTime;
    // 레벨 데이터 리스트
    public List<Level> levelList;
    public enum MonsterName { Pladog, Polipaca, Skun }
    public enum SpawnDirection { RightUp, LeftUp, RightDown, LeftDown }

} 

[System.Serializable]
public struct Level
{
    public WaveData.MonsterName monsterName;
    public WaveData.SpawnDirection spawnDirection;
    public int monsterNumber;
    public float nextTime;
}
