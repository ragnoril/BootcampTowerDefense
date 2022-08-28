using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event Action OnGameStarted;
    public event Action<bool> OnGameOver;

    public event Action<int> OnEnemyFinishedPath;
    public event Action<Enemy> OnEnemyKilled;
    public event Action<Enemy, int> OnEnemyHit;

    public event Action<TowerBase> OnBaseSelected;

    public event Action<Tower> OnTowerBuild;
    public event Action<Tower> OnTowerSelected;
    public event Action<Tower> OnTowerUpgraded;

    public event Action OnWaveStarted;
    public event Action OnWaveEnded;

    public event Action<int> OnCreditSpent;
    public event Action OnCreditChanged;

    public event Action OnHealthChanged;

    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public void GameOver(bool whoWon)
    {
        OnGameOver?.Invoke(whoWon);
    }

    public void EnemyFinishedPath(int damage)
    {
        OnEnemyFinishedPath?.Invoke(damage);
    }

    public void EnemyKilled(Enemy enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
    }

    public void EnemyHit(Enemy enemy, int damage)
    {
        OnEnemyHit?.Invoke(enemy, damage);
    }

    public void CreditSpent(int amount)
    {
        OnCreditSpent?.Invoke(amount);
    }

    public void CreditChanged()
    {
        OnCreditChanged?.Invoke();
    }

    public void TowerBaseSelected(TowerBase towerBase)
    {
        OnBaseSelected?.Invoke(towerBase);
    }

    public void TowerSelected(Tower tower)
    {
        OnTowerSelected?.Invoke(tower);
    }

    public void TowerBuilt(Tower tower)
    {
        OnTowerBuild?.Invoke(tower);
    }

    public void TowerUpgraded(Tower tower)
    {
        OnTowerUpgraded?.Invoke(tower);
    }

    public void WaveStarted()
    {
        OnWaveStarted?.Invoke();
    }

    public void WaveEnded()
    {
        OnWaveEnded?.Invoke();
    }

    public void HealthChanged()
    {
        OnHealthChanged?.Invoke();
    }

}
