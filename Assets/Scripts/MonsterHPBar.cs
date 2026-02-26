using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    public Image hpBarImage;
    public MonsterMove monster;
    public RectTransform hpBarCanvas;

    private void Start()
    {
        if (monster == null) 
        { 
            monster = GetComponent<MonsterMove>(); 
        }
    }

    private void Update()
    {
        if (monster != null && hpBarImage != null)
        {
            float healthRatio = (float)monster.hp / (float)monster.maxHp;

            hpBarImage.fillAmount = healthRatio;

            if (hpBarCanvas != null)
            {
                hpBarCanvas.LookAt(hpBarCanvas.position + Camera.main.transform.rotation * Vector3.forward,
                                   Camera.main.transform.rotation * Vector3.up);
            }
        }
    }
}
