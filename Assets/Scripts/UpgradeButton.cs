using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public string categoryName;
    public TextMeshProUGUI levelText; 

    private void Start()
    {
        UpdateLevelText();
    }
    public void OnClickUpgrade()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        GameManager.instance.UpgradeCategory(categoryName);
        UpdateLevelText();

    }

    public void UpdateLevelText()
    {
        if (levelText != null)
        {
            int currentLevel = GameManager.instance.GetUpgradeLevel(categoryName);
            levelText.text = "Lv. " + currentLevel;
        }
    }

}
