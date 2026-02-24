using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("타워 합성 설정")]
    public string towerName; //타워 이름
    public int tier = 1; //타워 등급
    public Node myNode;

    [Header("데이터 설정")]
    public TowerData data;

    [Header("타워 설정")]
    public float range = 3f; //사거리
    public LayerMask monsterLayer; //감지할 레이어
    public Transform visualRoot;
    public Transform firePoint;

    private Transform target;

    [Header("발사 설정")]
    public GameObject bulletPrefab;
    private float fireCountdown = 0f; //발사 쿨타임

    public float damageMultiplier = 1.0f;
    public float fireRateMultiplier = 1.0f;
    public float rangeMultiplier = 1.0f;

    public void UpgradeDamage() 
    {
        damageMultiplier += 0.2f; 
    }
    public void UpgradeFireRate()
    {
        fireRateMultiplier += 0.2f; 
    }
    public void UpgradeRange() 
    {
        rangeMultiplier += 0.2f;
    }

    void LookAtTarget(Transform target)
    {
        if (visualRoot == null) 
        {
            return;
        }
        visualRoot.LookAt(target);
        Vector3 rot = visualRoot.localEulerAngles;
        visualRoot.localEulerAngles = new Vector3(0, rot.y, 0);
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null || firePoint == null) 
        {
            return; 
        }
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletScript = bulletGO.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.See(target);
            bulletScript.towerDamage = Mathf.RoundToInt(data.damage * damageMultiplier);
            bulletScript.attackType = data.attackType;
            if (data.attackType == AttackType.Area)
            {
                bulletScript.explosionRadius = data.explosionRadius;
            }
            else
            {
                bulletScript.effectValue = data.effectValue;
            }
        }
    }
   
    void Update()
    {
        float currentRange = data.range * rangeMultiplier;
        Collider[] targets = Physics.OverlapSphere(transform.position, currentRange, monsterLayer);

        MonsterMove bestTarget = null;
        float maxDistance = -1f;

        for (int i = 0; i < targets.Length; i++)
        {
            MonsterMove monster = targets[i].GetComponent<MonsterMove>();
            if (monster != null)
            {
                float dist = monster.GetDistanceTraveled();
                if (dist > maxDistance)
                {
                    maxDistance = dist;
                    bestTarget = monster;
                }
            }
        }

        if (bestTarget != null)
        {
            Transform targetTransform = bestTarget.transform;

            LookAtTarget(targetTransform);

            float fireInterval = 1f / (data.fireRate * fireRateMultiplier);
            if (fireCountdown <= 0f)
            {
                Shoot(targetTransform);
                fireCountdown = fireInterval;
            }
        }
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
    }
    public void SplashDamage(Vector3 targetPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, data.explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                MonsterMove monster = hitCollider.GetComponent<MonsterMove>();

                if (monster != null)
                {
                    float finalDamage = data.damage * damageMultiplier;
                    monster.TakeDamage((int)finalDamage);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (data != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.range * rangeMultiplier);
        }
    }
}
