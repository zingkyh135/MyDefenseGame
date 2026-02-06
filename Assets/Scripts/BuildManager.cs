using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("등급별 타워 리스트")]
    public GameObject[] tier1Towers;
    public GameObject[] tier2Towers;
    public GameObject[] tier3Towers;
    public GameObject[] tier4Towers;
    public GameObject[] tier5Towers;

    [Header("건설 비용")]
    public int buildCost = 50; //타워 건설 비용

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    public GameObject BuildTowerOnNode(Vector3 position)
    {
        if (GameManager.instance.gold >= buildCost)
        {
            GameObject towerBuild = null;
            float randomValue = Random.value;

            if (randomValue < 0.70f)
            {
                towerBuild = GetRandomList(tier1Towers); //70% 확률 1티어
            }
            else if (randomValue < 0.95f)
            {
                towerBuild = GetRandomList(tier2Towers); //25% 확률 2티어
            }
            else
            {
                towerBuild = GetRandomList(tier3Towers); //5% 확률 3티어
            }

            if (towerBuild != null)
            {
                Vector3 spawnPos = new Vector3(position.x, position.y, position.z - 0.5f);
                Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

                GameObject newTower = Instantiate(towerBuild, spawnPos, spawnRotation);
                GameManager.instance.SpendGold(buildCost); //골드 소모
                Debug.Log("타워 건설");
            }
            return null;
        }
        else
        {
            Debug.Log("골드 부족");
            return null;
        }
    }
    private GameObject GetRandomList(GameObject[] list)
    {
        if(list == null || list.Length == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, list.Length);
        return list[randomIndex];
    }
    public void MergeTowers(Tower selectedTower)
    {
        Tower[] allTowers = FindObjectsOfType<Tower>();
        Tower partnerTower = null;

        for (int i = 0; i < allTowers.Length; i++)
        {
            Tower target = allTowers[i];

            //선택타워가 아니고 이름이 같으며 티어가 같을 때
            if (target != selectedTower && target.towerName == selectedTower.towerName && target.tier == selectedTower.tier)
            {
                partnerTower = target;
                break;
            }
        }
        if (partnerTower != null)
        {
            ExecuteMerge(selectedTower, partnerTower);
        }
        else
        {
            
        }
    }
    private void ExecuteMerge(Tower towerA, Tower towerB)
    {
        if (towerA.tier >= 5)
        {
            return;
        }

        int nextTier = towerA.tier + 1;
        GameObject nextTowerPrefab = null;

        if (nextTier == 2)
        {
            nextTowerPrefab = GetRandomList(tier2Towers);
        }
        else if (nextTier == 3)
        {
            nextTowerPrefab = GetRandomList(tier3Towers);
        }
        else if (nextTier == 4)
        {
            nextTowerPrefab = GetRandomList(tier4Towers);
        }
        else if (nextTier == 5)
        {
            nextTowerPrefab = GetRandomList(tier5Towers);
        }

        if (nextTowerPrefab != null)
        {
            Instantiate(nextTowerPrefab, towerA.transform.position, towerA.transform.rotation);
            Destroy(towerA.gameObject);
            Destroy(towerB.gameObject);
            Debug.LogFormat("{0}티어 합성", nextTier);
        }
    }
}