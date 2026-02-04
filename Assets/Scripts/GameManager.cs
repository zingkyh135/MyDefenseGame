using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("∞ÒµÂ º≥¡§")]
    public int gold = 100; //Ω√¿€ ∞ÒµÂ

    private void Awake()
    {
        instance = this;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("«ˆ¿Á ∞ÒµÂ:" + gold);
    }
}
