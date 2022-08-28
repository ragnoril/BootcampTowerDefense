using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public EventManager Events;
    public UIManager UI;

    public Level CurrentLevel;

    public TowerSO[] TowerList;

    public int Credits;

    public int MaxHealth;
    public int CurrentHealth;


    private void Start()
    {
        CurrentHealth = MaxHealth;
        Events.HealthChanged();

        Events.OnEnemyKilled += EnemyKilled;
        Events.OnEnemyFinishedPath += EnemyAttacks;
        Events.OnCreditSpent += CreditSpent;
        Events.OnTowerBuild += ActivateTower;
        Events.OnGameOver += GameOver;
    }
    private void OnDestroy()
    {
        Events.OnEnemyKilled -= EnemyKilled;
        Events.OnEnemyFinishedPath -= EnemyAttacks;
        Events.OnCreditSpent -= CreditSpent;
        Events.OnTowerBuild -= ActivateTower;
        Events.OnGameOver -= GameOver;
    }

    private void GameOver(bool whoWon)
    {
        if (whoWon)
            Debug.Log("Player WON!!!!");
        else
            Debug.Log("Player LOST!!!!");

    }

    private void EnemyKilled(Enemy enemy)
    {
        Credits += 150;
        Events.CreditChanged();
    }

    private void ActivateTower(Tower tower)
    {
        tower.InitTower();
    }

    private void CreditSpent(int amount)
    {
        Credits -= amount;
        Events.CreditChanged();
    }

    private void EnemyAttacks(int damage)
    {
        Debug.Log("Player hit for: " + damage);
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            PlayerDies();
    }

    private void PlayerDies()
    {
        Events.GameOver(false);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
