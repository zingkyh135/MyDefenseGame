using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [Header("몬스터 설정")]
    public string monsterName; //종 이름
    public int hp = 10; 
    public float speed = 2f; //이속

    public Transform[] waypoints; //몬스터 이동경로 설정
    int index = 0; //경로 번호

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
            Debug.LogWarning("Waypoints없음");
        }
    }
    private void Update()
    {
        if (index < waypoints.Length) //만약 번호가 경로 수보다 작으면
        {
            //다음 경로로 스피드만큼 이동
            transform.position = Vector3.MoveTowards(transform.position,waypoints[index].position, speed*Time.deltaTime);
            //만약 경로에 도착하면
            if(transform.position == waypoints[index].position )
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
