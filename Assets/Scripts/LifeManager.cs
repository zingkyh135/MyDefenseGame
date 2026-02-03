using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    [Header("설정")]
    public int life = 10; //시작 목숨
    public TextMeshProUGUI lifeText; // 화면 표시할 UI 텍스트

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
        UpdateLifeUI();
    }

    // 목숨을 깎는 함수
    public void DecreaseLife()
    {
        life--;
        UpdateLifeUI();

        if (life <= 0)
        {
            Debug.Log("게임 오버!");
            // 나중에 게임 오버 팝업 연동
        }
    }
}
