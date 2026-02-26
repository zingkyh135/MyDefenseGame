using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.Examples.ObjectSpin;

[System.Serializable]
public class UpgradeData
{
    public int damageLevel = 0;
    public int rangeLevel = 0;
    public int speedLevel = 0;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Dictionary<string, UpgradeData> upgradeLevels = new Dictionary<string, UpgradeData>();

    [Header("골드 설정")]
    public int gold = 100; //시작 골드
    public TextMeshProUGUI goldText;

    [Header("다이아 설정")]
    public int diamond = 0; // 강화 재화
    public TextMeshProUGUI diamondText;

    public int currentWave = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public int GetUpgradeLevel(string categoryName)
    {
        return 0;
    }
    public int GetOptionLevel(string category, string optionType)
    {
        if (!upgradeLevels.ContainsKey(category)) 
        { 
            return 0; 
        }

        UpgradeData data = upgradeLevels[category];
        if (optionType == "Damage") 
        { 
            return data.damageLevel; 
        }
        if (optionType == "Range")
        { 
            return data.rangeLevel;
        }
        if (optionType == "Speed") 
        { 
            return data.speedLevel; 
        }
        return 0;
    }
    public void SetWave(int wave)
    {
        currentWave = wave;
    }

    public void UpgradeCategory(string categoryName, bool isDiamond, string optionType)
    {
        int cost;

        if (isDiamond == true)
        {
            cost = 1;
        }
        else
        {
            cost = 50;
        }
        bool canUpgrade = false;
        if (isDiamond == true)
        {
            canUpgrade = SpendDiamond(cost);
        }
        else
        {
            canUpgrade = SpendGold(cost);
        }
        if (canUpgrade == true)
        {
            if (upgradeLevels.ContainsKey(categoryName) == false)
            {
                upgradeLevels[categoryName] = new UpgradeData();
            }
            if (optionType == "Damage")
            {
                upgradeLevels[categoryName].damageLevel = upgradeLevels[categoryName].damageLevel + 1;
            }
            else if (optionType == "Range")
            {
                upgradeLevels[categoryName].rangeLevel = upgradeLevels[categoryName].rangeLevel + 1;
            }
            else if (optionType == "Speed")
            {
                upgradeLevels[categoryName].speedLevel = upgradeLevels[categoryName].speedLevel + 1;
            }

            if (BuildManager.instance != null)
            {
                BuildManager.instance.RefreshAllTowers();
            }
        }
    }
    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold;
        }
    }
    void UpdateDiamondUI()
    {
        if (diamondText != null)
        {
            diamondText.text = "Diamond: " + diamond;
        }
    }
    void Start()
    {
        UpdateGoldUI();
        UpdateDiamondUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }
    public bool SpendGold(int amount)
    {
        if (gold < amount)
        {
            return false;
        }
        gold -= amount;
        UpdateGoldUI();
        return true;
    }
    public void AddDiamond(int amount)
    {
        diamond += amount;
        UpdateDiamondUI();
    }

    public bool SpendDiamond(int amount)
    {
        if (diamond >= amount)
        {
            diamond -= amount;
            UpdateDiamondUI();
            return true;
        }
        return false;
    }
}
