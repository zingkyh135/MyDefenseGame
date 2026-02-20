using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("타워 정보창")]
    public GameObject infoPanel;
    private Tower currentTower;

    private void Awake()
    {
        if (instance != null) 
        { 
            return;
        }
        instance = this;
    }
    public void ShowTower(Tower tower)
    {
        currentTower = tower;
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }
    public void OnClickMergeButton()
    {
        if (currentTower != null)
        {
            BuildManager.instance.MergeTowers(currentTower);
            infoPanel.SetActive(false); //합성 후 창 닫기
        }
    }
    public void OnClickSellButton()
    {
        if (currentTower != null)
        {
            BuildManager.instance.SellTower(currentTower);
            infoPanel.SetActive(false); //판매 후 창 닫기
        }
    }
    public void OnClickCloseButton()
    {
        CloseInfoPanel();
    }

    public void CloseInfoPanel()
    {
        currentTower = null;
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
        }
    }

}
