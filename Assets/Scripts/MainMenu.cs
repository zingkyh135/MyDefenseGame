using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI bestWaveText;

    void Start()
    {
        int bestWave = PlayerPrefs.GetInt("BestWave", 0);

        if (bestWaveText != null)
        {
            bestWaveText.text = "최고 웨이브: " + bestWave;
        }
        Time.timeScale = 1f;
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(SceneNames.GameScene);
    }

    // [버튼 연결용] 게임 종료 함수
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
