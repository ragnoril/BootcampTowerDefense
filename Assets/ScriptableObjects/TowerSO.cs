using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TowerDefense/New Tower")]
public class TowerSO : ScriptableObject
{
    public string TowerName;
    //public int Health;
    public int Damage;
    public int Cost;
    public float Range;
    public float AttackRate;

    public GameObject Prefab;
}
