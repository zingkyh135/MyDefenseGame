using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    [Header("설정")]
    public int life = 10; //시작 목숨
    public TextMeshProUGUI lifeText; //화면에 표시되는 라이프 텍스트
    public GameObject gameOverPanel; //게임오버 시 나오는 창

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void UpdateLifeUI()
    {
        if (lifeText != null)
        {
            
            lifeText.text = "LIFE: " + life;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateLifeUI();
    }

    // 목숨을 깎는 함수
    public void DecreaseLife()
    {
        if (life <= 0)
        {
            return;
        }

        life--;
        UpdateLifeUI();

        if (life <= 0)
        {
            DoGameOver();
        }
    }
    void DoGameOver()
    {
        Debug.Log("게임오버");
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
