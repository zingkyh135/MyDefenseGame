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
            damageUpgradeBtn.SetActive(tower.data.enhancementCategory == EnhancementCategory.DamageUpgrade);
            speedUpgradeBtn.SetActive(tower.data.enhancementCategory == EnhancementCategory.SpeedUpgrade);
            rangeUpgradeBtn.SetActive(tower.data.enhancementCategory == EnhancementCategory.RangeUpgrade);
        }
    }
    public void OnClickUpgradeDamage()
    {
        TryUpgrade(() => currentTower.UpgradeDamage());
    }

    public void OnClickUpgradeSpeed()
    {
        TryUpgrade(() => currentTower.UpgradeFireRate());
    }

    public void OnClickUpgradeRange()
    {
        TryUpgrade(() => currentTower.UpgradeRange());
    }
    private void TryUpgrade(System.Action upgradeAction)
    {
        int upgradeCost = 50;

        if (currentTower != null && GameManager.instance.SpendDiamond(upgradeCost))
        {
            upgradeAction.Invoke();
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
