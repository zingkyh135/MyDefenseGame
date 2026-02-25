using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType 
{
    Normal, Slow, Dot, Stun, PercentDamage, Area
}

[CreateAssetMenu(fileName = "NewTowerData", menuName = "Tower/Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    [SerializeField] public int damage;
    public float fireRate;
    public float range;
    public AttackType attackType;
    public float effectValue;
    public float explosionRadius = 2.0f;
    public string attribute;
}
