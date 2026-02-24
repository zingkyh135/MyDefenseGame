using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("설정")]
    public float speed = 10f; //총알 속도
    public int towerDamage; //타워 공격력
    public AttackType attackType;
    public float effectValue;
    private Transform target; //타겟
    public float explosionRadius;

    public void See(Transform monster)
    {
        target = monster;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (attackType == AttackType.Area)
            {
                Explode();
            }
            else
            {
                MonsterMove monsterScript = other.GetComponent<MonsterMove>();
                if (monsterScript != null)
                {
                    ApplyEffect(monsterScript);
                }
            }
            Destroy(gameObject);
        }
    }
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                MonsterMove monster = hitCollider.GetComponent<MonsterMove>();
                if (monster != null)
                {
                    ApplyEffect(monster);
                }
            }
        }
    }
    private void ApplyEffect(MonsterMove monster)
    {
        monster.TakeDamage(towerDamage);

        switch (attackType)
        {
            case AttackType.Slow:
                //슬로우 수치를 전달하여 속도 감소
                monster.ApplySlow(effectValue);
                break;
            case AttackType.Stun:
                //확률적으로 스턴 적용
                if (Random.value < effectValue) monster.ApplyStun();
                break;
            case AttackType.Dot:
                //도트 데미지 코루틴 실행
                float calculatedDotDamage = towerDamage * effectValue;
                monster.ApplyDotDamage(effectValue);
                break;
            case AttackType.PercentDamage:
                monster.ApplyPercentDamage(effectValue);
                break;
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
