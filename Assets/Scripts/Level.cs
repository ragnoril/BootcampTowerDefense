using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform SpawnPosition;
    public Transform[] Waypoints;
    public WaveSO[] WaveList;

    public List<GameObject> Enemies;

    public int WaveCount;
    public bool IsWaveStarted;

    private float _spawnTimer;
    private int _waveEnemyIndex;

    private void Start()
    {
        Enemies = new List<GameObject>();

        GameManager.Instance.Events.OnEnemyKilled += CheckWave;

        WaveCount = 0;
        StartNewWave();
    }
    private void OnDestroy()
    {
        GameManager.Instance.Events.OnEnemyKilled -= CheckWave;
    }

    private void CheckWave(Enemy enemy)
    {
        if (Enemies.Contains(enemy.gameObject))
            Enemies.Remove(enemy.gameObject);

        if (WaveCount >= WaveList.Length)
            return;

        if (Enemies.Count == 0 && _waveEnemyIndex == WaveList[WaveCount].EnemyList.Length)
        {
            GameManager.Instance.Events.WaveEnded();

            WaveCount += 1;
            if (WaveCount < WaveList.Length)
            {
                StartNewWave();
            }
            else
            {
                GameManager.Instance.Events.GameOver(true);
            }
        }
    }

    private void StartNewWave()
    {
        _spawnTimer = 0f;
        _waveEnemyIndex = 0;
        IsWaveStarted = true;
        GameManager.Instance.Events.WaveStarted();

        Debug.Log("New Wave started");
    }

    private void Update()
    {
        if (WaveCount >= WaveList.Length)
            return;

        if (IsWaveStarted)
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer < 0f)
            {
                _spawnTimer = WaveList[WaveCount].SpawnRate;
                SpawnEnemy(_waveEnemyIndex);
                _waveEnemyIndex += 1;

                if (_waveEnemyIndex == WaveList[WaveCount].EnemyList.Length)
                    IsWaveStarted = false;
            }

        }

    }

    private void SpawnEnemy(int waveEnemyIndex)
    {
        GameObject go = Instantiate(WaveList[WaveCount].EnemyList[_waveEnemyIndex].Prefab, SpawnPosition.position, Quaternion.identity);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.Data = WaveList[WaveCount].EnemyList[_waveEnemyIndex];
        enemy.InitMe();
        Enemies.Add(go);
    }

    public Transform GetWaypoint(int index)
    {
        if (index < Waypoints.Length)
            return Waypoints[index];
        else
            return null;
    }
}
