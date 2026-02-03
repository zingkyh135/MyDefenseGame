using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab; // 소환할 몬스터 프리팹
    public Transform spawnPoint;     // 소환될 위치 (Point_0)

    public void SpawnMonster()
    {
        // 몬스터를 소환 지점에 생성
        Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
    }
}
