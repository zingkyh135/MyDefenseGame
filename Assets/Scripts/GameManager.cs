using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    [Header("골드 설정")]
    public int gold = 100; //시작 골드
    public TextMeshProUGUI goldText;

    [Header("다이아 설정")]
    public int diamond = 0; // 강화 재화
    public TextMeshProUGUI diamondText;

    private void Awake()
    {
        instance = this;
    }
    public int GetUpgradeLevel(string categoryName)
    {
        if (upgradeLevels.ContainsKey(categoryName))
        {
            return upgradeLevels[categoryName];
        }
        else
        {
            return 0;
        }
    }
    public void UpgradeCategory(string categoryName)
    {
        if (SpendDiamond(50)) // 50 다이아 고정 소모
        {
            if (!upgradeLevels.ContainsKey(categoryName)) 
            { 
                upgradeLevels[categoryName] = 0;
            }
            upgradeLevels[categoryName]++;
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
