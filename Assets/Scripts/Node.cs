using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour, IPointerClickHandler
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFull)
        {
            UIManager.instance.ShowTower(tower.GetComponent<Tower>());
            return;
        }

        GameObject newTower = BuildManager.instance.BuildTowerOnNode(transform.position, this);
        if (newTower != null)
        {
            tower = newTower;
        }
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
