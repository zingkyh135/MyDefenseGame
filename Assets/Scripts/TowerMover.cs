using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMover : MonoBehaviour
{
    private Tower tower;
    void Awake()
    {
        tower = GetComponent<Tower>();
    }
    void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
        { 
            return; 
        }

        if (UIManager.instance != null && tower != null)
        {
            UIManager.instance.ShowTower(tower);
        }
    }
}
