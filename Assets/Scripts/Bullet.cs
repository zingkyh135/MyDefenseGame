using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("설정")]
    public float speed = 10f; //총알 속도
    public int towerDamage; //타워 공격력
    private Transform target; //타겟

    public void See(Transform monster)
    {
        target = monster;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterMove monsterScript = other.GetComponent<MonsterMove>();
            if (monsterScript != null)
            {
                monsterScript.TakeDamage(towerDamage);
            }
            Debug.Log(other.name + " 명중!");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        transform.LookAt(target);
    }
}
