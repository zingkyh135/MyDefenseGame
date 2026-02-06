using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    private Tower tower;
    void Awake()
    {
        tower = GetComponent<Tower>();
    }
    void OnMouseDown()
    {
        Debug.Log("클릭 성공");
        if (UIManager.instance != null && tower != null) //만약 UIManager이 널이 아니고 타워도 널이 아닐때
        {
            UIManager.instance.ShowTower(tower);

            Debug.Log(tower.towerName + " 정보창");
        }
        else
        {
            Debug.Log("클릭만 성공");
        }
    }
}
