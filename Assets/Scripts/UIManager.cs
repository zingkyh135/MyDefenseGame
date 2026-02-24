using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("타워 정보창")]
    public GameObject infoPanel;
    private Tower currentTower;

    [Header("강화 UI")]
    public GameObject upgradePanel;
    public GameObject damageUpgradeBtn;
    public GameObject speedUpgradeBtn;
    public GameObject rangeUpgradeBtn;

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
            damageUpgradeBtn.SetActive(true);
            speedUpgradeBtn.SetActive(true);
            rangeUpgradeBtn.SetActive(true);
        }
    }
    public void OnClickUpgradeDamage()
    {
        UpgradeGroup("DamageGroup");
    }

    public void OnClickUpgradeSpeed()
    {
        UpgradeGroup("SpeedGroup");
    }

    public void OnClickUpgradeRange()
    {
        UpgradeGroup("RangeGroup");
    }
    private void UpgradeGroup(string groupName)
    {
        int upgradeCost = 50;

        if (GameManager.instance.SpendDiamond(upgradeCost))
        {
            GameManager.instance.UpgradeCategory(groupName);

            BuildManager.instance.RefreshAllTowers();
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
