using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNames
{
    public const string MainMenu = "MainMenu";
    public const string GameScene = "GameScene";
}
public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    [Header("설정")]
    public int life = 10; //시작 목숨
    public TextMeshProUGUI lifeText; //화면에 표시되는 라이프 텍스트
    public GameObject gameOverPanel; //게임오버 시 나오는 창

    [Header("결과 화면 UI")]
    public TextMeshProUGUI resultCurrentWaveText; // 게임오버 창에 띄울 현재 웨이브
    public TextMeshProUGUI resultBestWaveText;    // 게임오버 창에 띄울 최고 기록

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
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            int currentWave = (GameManager.instance != null) ? GameManager.instance.currentWave : 0;

            int bestWave = PlayerPrefs.GetInt("BestWave", 0);

            if (currentWave > bestWave)
            {
                bestWave = currentWave;
                PlayerPrefs.SetInt("BestWave", bestWave);
                PlayerPrefs.Save();
            }

            // 4. UI 텍스트에 출력
            if (resultCurrentWaveText != null) 
            { 
                resultCurrentWaveText.text = "현재 웨이브: " + currentWave;
            }
            if (resultBestWaveText != null)
            {
                resultBestWaveText.text = "최고 웨이브: " + bestWave;
            }
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.GameScene);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
