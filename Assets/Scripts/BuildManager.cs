using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("설정")]
    public GameObject towerPrefab; //지을 타워
    public int buildCost = 50; //타워 건설 비용

    void Awake()
    {
        instance = this;
    }

    public GameObject BuildTowerOnNode(Vector3 position)
    {


        if (GameManager.instance.gold >= buildCost)
        {
            Vector3 spawnPos = new Vector3(position.x, position.y, position.z - 0.5f);
            Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

            GameObject newTower = Instantiate(towerPrefab, spawnPos, spawnRotation);
            GameManager.instance.SpendGold(buildCost);
            Debug.Log("타워 건설");
            return newTower;
        }
        else
        {
            Debug.Log("골드 부족");
            return null;
        }

    }
}
