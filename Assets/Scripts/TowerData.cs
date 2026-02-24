using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnhancementCategory
{
    DamageUpgrade, //데미지, 기본, 퍼뎀
    SpeedUpgrade,  //범위, 스턴, 슬로우
    RangeUpgrade   //사거리, 공속, 도트
}
public enum AttackType 
{
    Normal, Slow, Dot, Stun, PercentDamage, Area
}

[CreateAssetMenu(fileName = "NewTowerData", menuName = "Tower/Data")]
public class TowerData : ScriptableObject
{
    public string towerName; 
    public int damage;
    public float fireRate;
    public float range;
    public AttackType attackType;
    public float effectValue;
    public float explosionRadius = 2.0f;
    public EnhancementCategory enhancementCategory;
}
