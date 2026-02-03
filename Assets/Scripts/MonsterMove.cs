using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Transform[] waypoints; //몬스터 이동경로 설정
    public float speed = 2f; //이동 속도
    int index = 0; //경로 번호

    private void Start()
    {
        //하이어라키에서 "Waypoints"라는 이름의 부모 오브젝트를 찾음
        GameObject waypointGroup = GameObject.Find("Waypoints");

        if (waypointGroup != null)
        {
            //자식(Point_0, Point_1 등)의 개수만큼 배열 크기를 결정
            waypoints = new Transform[waypointGroup.transform.childCount];

            //자식들의 위치(Transform) 정보를 하나씩 꺼내서 배열에 추가
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
            Destroy(gameObject); //파괴
        }
    }
}
