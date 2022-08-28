using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefense/New Wave")]
public class WaveSO : ScriptableObject
{
    public EnemySO[] EnemyList;
    public float SpawnRate;
}
