using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("타워 정보창")]
    public GameObject infoPanel;
    private Tower currentTower;

    [Header("강화 선택창")]
    public GameObject upgradeChoicePanel;
    private string selectedCategory;
    private bool isSelectedDiamond;

    [Header("일시정지창")]
    public GameObject pausePanel;
    private bool isPaused = false;

    private void Awake()
    {
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
    public void ShowUpgradeChoicePanel(string categoryName, bool isDiamond)
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

        bool canAfford;

        if (isDiamond == true)
        {
            canAfford = (GameManager.instance.diamond >= cost);
        }
        else
        {
            canAfford = (GameManager.instance.gold >= cost);
        }

        if (canAfford)
        {
            selectedCategory = categoryName;
            isSelectedDiamond = isDiamond;
            if (upgradeChoicePanel != null)
            {
                upgradeChoicePanel.SetActive(true);
            }
        }
    }
    public void OnClickChoiceOption(string optionType)
    {
        GameManager.instance.UpgradeCategory(selectedCategory, isSelectedDiamond, optionType);
        upgradeChoicePanel.SetActive(false);
    }
    public void OnClickConfirmUpgrade()
    {
        GameManager.instance.UpgradeCategory(selectedCategory, isSelectedDiamond, "Damage");

        if (upgradeChoicePanel != null) 
        { 
            upgradeChoicePanel.SetActive(false); 
        }
    }
    public void OnClickCloseChoicePanel()
    {
        if (upgradeChoicePanel != null) 
        {
            upgradeChoicePanel.SetActive(false);
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
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
