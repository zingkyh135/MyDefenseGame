using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject infoPanel;
    private Tower currentTower;

    private void Awake()
    {
        instance = this;
    }
    public void ShowTower(Tower tower)
    {
        currentTower = tower;
        infoPanel.SetActive(true);
    }
    public void OnClickMergeButton()
    {
        if (currentTower != null)
        {
            // 드디어 빌드매니저의 'TryMergeTower'를 호출!
            BuildManager.instance.MergeTowers(currentTower);
            infoPanel.SetActive(false); // 합성 후 창 닫기
        }
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
            Application.Quit();
        }
    }

}
