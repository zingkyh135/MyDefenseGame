using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.cyan; // 마우스 올렸을 때 색상
    private Color startColor;
    private SpriteRenderer rend;

    public GameObject tower; // 현재 이 자리에 세워진 타워

    public bool isFull => tower != null;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        if (rend != null)
        {
            startColor = rend.color;
        }
    }

    // 마우스를 클릭했을 때
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;
        }
        //타워가 이미 있다면
        if (isFull)
        {
            return;
        }
        tower = BuildManager.instance.BuildTowerOnNode(transform.position, this);
    }
    public void ClearNode()
    {
        tower = null;
    }

    //마우스를 올리면 색이 변함
    void OnMouseEnter()
    {
        if (rend != null && !isFull)
        {
            rend.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (rend != null)
        {
            rend.color = startColor;
        }
    }
}
