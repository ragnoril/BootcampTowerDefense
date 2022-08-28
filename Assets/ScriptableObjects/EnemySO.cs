using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefense/New Enemy")]
public class EnemySO : ScriptableObject
{
    public string EnemyName;
    public int Health;
    public int Damage;
    public int Speed;

    public GameObject Prefab;
}
