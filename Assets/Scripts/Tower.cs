using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("타워 합성 설정")]
    public string towerName; //타워 이름
    public int tier = 1; //타워 등급
    public Node myNode;

    [Header("타워 설정")]
    public float range = 3f; //사거리
    public LayerMask monsterLayer; //감지할 레이어
    public Transform visualRoot;
    public Transform firePoint;

    private Transform target;

    [Header("발사 설정")]
    public GameObject bulletPrefab;
    public float fireRate = 1f; //발사 속도
    public int damage = 10; //타워 공격력
    private float fireCountdown = 0f; //발사 쿨타임

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
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletScript = bulletGO.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.See(target);
            bulletScript.towerDamage = damage;
        }
    }
   
    void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, monsterLayer);

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
            Debug.Log("가장 앞 적" + targetTransform.name);

            if (fireCountdown <= 0f)
            {
                Shoot(targetTransform); // Shoot 함수에도 선별된 타겟 전달
                fireCountdown = 1f / fireRate;
            }
        }
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
