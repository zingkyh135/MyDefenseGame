using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("∞ÒµÂ º≥¡§")]
    public int gold = 100; //Ω√¿€ ∞ÒµÂ
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        instance = this;
    }
    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold;
        }
    }
    void Start()
    {
        UpdateGoldUI();
    }
    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("«ˆ¿Á ∞ÒµÂ:" + gold);
        UpdateGoldUI();
    }
    public void SpendGold(int amount)
    {
        gold -= amount;
        UpdateGoldUI();
    }
}
