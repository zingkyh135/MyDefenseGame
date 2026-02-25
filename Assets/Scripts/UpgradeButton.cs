using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [Header("강화 설정")]
    public string categoryName;
    public bool isDiamond;

    public TextMeshProUGUI levelText; 

    private void Start()
    {
        UpdateLevelText();
    }
    public void OnClickUpgrade()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowUpgradeChoicePanel(categoryName, isDiamond);

            UpdateLevelText();
        }
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
