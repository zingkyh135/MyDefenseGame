using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("소환할 몬스터")]
    public GameObject[] normalMonsterPrefabs;
    public GameObject[] bossPrefabs;

    [Header("시간 설정")]
    public float firstWaveDelay = 3f; //시작 대기 시간
    public float timeBetweenWaves = 3f; //다음 웨이브 대기 시간
    public float timeBetweenEnemies = 0.5f; //몬스터 소환 간격

    [Header("몬스터 설정")]
    public int monstersPerWave = 15; //웨이브 몬스터 수
    public Transform spawnPoint; //소환될 위치 (Point_0)

    [Header("UI")]
    public TextMeshProUGUI waveText; //현재 웨이브
    public TextMeshProUGUI timerText; //다음 타이머

    private int waveIndex = 0; //현제 웨이브 번호
    private int monstersToSpawn = 0; //웨이브에 남은 몬스터 수
    public int enemiesAlive = 0; //현재 남은 몬스터 수
    private float spawnTimer = 0f; //몬스터 소환 시간
    private float waveWaitTimer = 0f; //웨이브 대기 시간

    private bool isWaving = false; //현재 웨이브 소환 상태
    private bool isWaitingNextWave = false; //웨이브 대기 상태

    private GameObject currentWavePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void PrepareNextWave(float delay)
    {
        isWaitingNextWave = true;
        waveWaitTimer = delay;
    }

    private void Start()
    {
        PrepareNextWave(firstWaveDelay);
    }
    void StartNewWave()
    {
        isWaitingNextWave = false;
        isWaving = true;
        waveIndex++;

        if (waveIndex % 10 == 0) //10단위 보스 웨이브
        {
            if (bossPrefabs != null && bossPrefabs.Length > 0)
            {
                int bossIndex = (waveIndex / 10 - 1) % bossPrefabs.Length;
                currentWavePrefab = bossPrefabs[bossIndex];
            }
            monstersToSpawn = 1; //보스는 1마리
            Debug.Log("보스 웨이브 시작");
        }
        else
        {
            if (normalMonsterPrefabs != null && normalMonsterPrefabs.Length > 0)
            {
                int prefabIndex = (waveIndex - 1 - (waveIndex / 10)) % normalMonsterPrefabs.Length;

                currentWavePrefab = normalMonsterPrefabs[prefabIndex];
            }
            monstersToSpawn = monstersPerWave;
        }

        UpdateWaveUI();
        spawnTimer = timeBetweenEnemies; //첫 번째 몬스터 소환을 위해 초기화
    }
    private void Update()
    {
        //다음 웨이브 대기 상태
        if (isWaitingNextWave)
        {
            waveWaitTimer -= Time.deltaTime;

            //타이머 UI표시
            if (timerText != null)
            {
                timerText.text = "Next Wave in: " + Mathf.Ceil(waveWaitTimer);
            }
            if (waveWaitTimer <= 0)
            {
                StartNewWave();
            }
        }

        //웨이브 진행 상태
        if (isWaving)
        {
            spawnTimer += Time.deltaTime;

            if (monstersToSpawn > 0)
            {
                if (spawnTimer >= timeBetweenEnemies)
                {
                    SpawnMonster();
                    monstersToSpawn--;
                    spawnTimer = 0f;
                }
            }
            //모든 몬스터 소환 및 살아있는 몬스터가 0일 때
            if (monstersToSpawn <= 0 && enemiesAlive <= 0)
                {
                    isWaving = false;
                    //웨이브 종료 후 3초 뒤 자동으로 다음 웨이브 시작
                    PrepareNextWave(timeBetweenWaves);
                }
            
        }
    }
    void SpawnMonster()
    {
        if (currentWavePrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("프리팹 혹은 소환 위치(SpawnPoint)가 설정되지 않았습니다!");
            return;
        }

        GameObject waypointGroup = GameObject.Find("Waypoints");
        Vector3 firstDest = waypointGroup.transform.GetChild(0).position;
        Vector3 direction = (firstDest - spawnPoint.position).normalized;
        direction.z = 0;
        Quaternion spawnRotation = Quaternion.LookRotation(direction, Vector3.back);

        GameObject enemy = Instantiate(currentWavePrefab, spawnPoint.position, spawnRotation);
        enemiesAlive++;

        // 몬스터 체력 고도화 (웨이브에 비례)
        MonsterMove monsterScript = enemy.GetComponent<MonsterMove>();
        if (monsterScript != null)
        {
            // 예: 기본 체력에 웨이브당 10씩 추가
            float growthMultiplier = 1f + (waveIndex - 1) * 0.2f; //웨이브당 20%씩 강화
            monsterScript.hp = Mathf.RoundToInt(monsterScript.hp * growthMultiplier);

            // 보스일 경우 체력을 10배로 설정
            if (waveIndex % 10 == 0)
            {
                monsterScript.hp *= 10;
            }
        }
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "WAVE " + waveIndex;
        }
        if (timerText != null)
        {
            timerText.text = ""; //웨이브 중엔 타이머 문구 삭제
        }
    }

    public void SkipToNextWave() //추후 스킵버튼 구현 고민중
    {
        if (isWaitingNextWave)
        {
            waveWaitTimer = 0; //대기 시간을 0으로 만들어 즉시 시작
        }
    }
}
