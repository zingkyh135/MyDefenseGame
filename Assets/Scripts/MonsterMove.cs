using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [Header("몬스터 설정")]
    public int hp = 10; 
    public float speed = 2f; //이속

    [Header("사망 시 분열 설정")]
    public bool canSplit = false; //분열 여부
    public GameObject splitPrefab; //분열 시 출현 몬스터
    public int splitCount = 2; //분열 수

    [Header("처치 재화 설정")]
    public int goldReward = 10;
    public int diamondReward = 0;

    public Transform[] waypoints; //몬스터 이동경로 설정
    public int index = 0; //경로 번호
    private float defaultSpeed; //원래속도
    private bool isStunned = false; //스턴 상태
    private Coroutine dotCoroutine;

    private float distanceTraveled = 0f;

    private void Awake()
    {
        defaultSpeed = speed; //초기 속도 저장
    }

    public float GetDistanceTraveled()
    {
        return distanceTraveled;
    }

    public void ApplySlow(float slowAmount)
    {
        speed = defaultSpeed * (1f - slowAmount);
        StartCoroutine(ResetSpeed(2f));//일정 시간 후 원래 속도로 복구
    }
    private IEnumerator ResetSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = defaultSpeed;
    }
    public void ApplyStun()
    {
        if (isStunned) //스턴 중복 방지
        {
            return;
        }
        isStunned = true;
        StartCoroutine(ResetStun(1.5f)); //1.5초 스턴
    }
    private IEnumerator ResetStun(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStunned = false;
    }
    public void ApplyDotDamage(float dotValue)
    {
        if (dotCoroutine != null)
        {
            StopCoroutine(dotCoroutine);
        }
        dotCoroutine = StartCoroutine(DotDamageCoroutine(dotValue, 3f));
    }
    private IEnumerator DotDamageCoroutine(float damagePerSecond, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage((int)damagePerSecond);
            yield return new WaitForSeconds(1f); //1초마다 데미지
            elapsed += 1f;
        }
        dotCoroutine = null;
    }
    public void ApplyPercentDamage(float percent)
    {
        int damage = Mathf.RoundToInt(hp * percent);

        if (damage < 1) damage = 1;

        TakeDamage(damage);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddGold(goldReward); //게임메니저 AddGold실행 gold + goldReward
            if (diamondReward > 0)
            {
                GameManager.instance.AddDiamond(diamondReward);
            }
        }

        if (canSplit && splitPrefab != null) //만약 분열 가능하고 프리펩이 있을 때
        {
            for(int i = 0; i < splitCount; i++)
            {
                GameObject miniMonster = Instantiate(splitPrefab, transform.position, Quaternion.identity);
                if (SpawnManager.instance != null)
                {
                    SpawnManager.instance.enemiesAlive++;
                }
                    //작은 몬스터는 현제 위치에 스폰
                MonsterMove miniScript = miniMonster.GetComponent<MonsterMove>(); //분열체는 MonsterMove를 받음
                if (miniScript != null)
                {
                    miniScript.index = this.index; //분열체의 경로는 현제 경로
                }
            }
        }
        Destroy(gameObject); 
    }
    private void OnDestroy()
    {
        if (SpawnManager.instance != null)
        {
            SpawnManager.instance.enemiesAlive--;
        }
    }

    private void Start()
    {
        //Waypoints 오브젝트 
        GameObject waypointGroup = GameObject.Find("Waypoints");

        if (waypointGroup != null)
        {
            //Point_ 개수만큼 배열 크기를 결정
            waypoints = new Transform[waypointGroup.transform.childCount];

            //위치 정보를 하나씩 배열에 추가
            for (int i = 0; i < waypointGroup.transform.childCount; i++)
            {
                waypoints[i] = waypointGroup.transform.GetChild(i);
            }
        }
        else
        {
        }
        //transform.rotation = Quaternion.Euler(0, 0, 180);
    }
    private void Update()
    {
        if (isStunned) 
        { 
            return;
        }
        if (waypoints == null || waypoints.Length == 0) 
        {
            return; 
        }
        if (index < waypoints.Length) //만약 번호가 경로 수보다 작으면
         {
            Vector3 targetPos = waypoints[index].position;
            Vector3 direction = targetPos - transform.position;
            direction.z = 0;

            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(direction, Vector3.back);
            }

            float moveStep = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveStep);
            distanceTraveled += moveStep;

            //만약 경로에 도착하면
            if (transform.position == waypoints[index].position)
            {
                index++; //다음 경로로 이동
            }
        }
        else //경로 번호보다 높거나 같으면
        {
            if (LifeManager.instance != null)
            {
                LifeManager.instance.DecreaseLife();
            }
            Destroy(gameObject); //파괴
        }
    }
}
